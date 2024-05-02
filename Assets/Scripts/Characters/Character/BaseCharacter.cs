using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BaseCharacter : MonoBehaviour
{
    public Define.ECharacterType _id = Define.ECharacterType.ECT_Empty;

    private BTMachine _btMachine;

    private AbstractAttack _abstractAttack;
    private AbstractSkill _abstractSkill;
    private AbstractMove _abstractMove;

    private EffectController _effectController;

    private Stat _stat;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    public event Action<BaseCharacter> DeathActionEvent;
    
    
    private void Awake()
    {
        _effectController = GetComponent<EffectController>();
        _stat = GetComponent<Stat>();

        _animator = GetComponentInChildren<Animator>(true);
        _rigidbody = GetComponent<Rigidbody2D>();
        
        InitStrategy();
    }

    private void InitStrategy()
    {
        _abstractAttack = GetComponent<AbstractAttack>();
        _abstractAttack?.Init(this, _stat);
        _abstractSkill = GetComponent<AbstractSkill>();
        _abstractSkill?.Init(this, _stat);
        _abstractMove = GetComponent<AbstractMove>();
        _abstractMove?.Init(this, _stat);
    }

    public void SetBehaviourTree(BTMachine btMachine)
    {
        _btMachine = btMachine;
    }

    private void Update()
    {
        _btMachine?.Operate();
    }
    //공격 이벤트
    public void Attack()
    {
        PlayAnimation("attack");
        _abstractAttack?.Attack();
    }

    public void UseSkill()
    {
        PlayAnimation("attack");
        _abstractSkill?.UseSkill();
    }
    //이동 이벤트
    public bool Move()
    {
        if (!this.IsAnimationPlaying("walk"))
        {
            PlayAnimation("walk");
        }
        return _abstractMove.Move();
    }

    public void Victory()
    {
        PlayAnimation("victory");
    }

    public void Hurt(float damage)
    {
        _stat.Hp -= damage;
        if (_stat.Hp <= 0)
        {
            Dead();
        }
    }

    public void Idle()
    {
        PlayAnimation("idle");
    }
    
    public void Dead()
    {
        PlayAnimation("death");
        _abstractMove.ClearTarget();
        _effectController.Clear();
        DeathActionEvent?.Invoke(this);
    }

    public void AnimationDeadEnd()
    {
        gameObject.SetActive(false);
    }
    
    public void Revive()
    {
        _stat.Hp = _stat.MaxHp;
        this.gameObject.SetActive(true);
    }
    
    // 버프 및 디버프 처리
    public void SetBuff(IEffect effect)
    {
        if (effect == null)
        {
            return;
        }
        _effectController.Add(effect);
    }

    private void PlayAnimation(string name)
    {
        _animator.Play(name, -1, 0f);
    }
    
    public bool IsDead() => _stat.Hp <= 0;
    public bool IsAnimationPlaying(string animName) => _animator.GetCurrentAnimatorStateInfo(0).IsName(animName);
    public bool IsAttackCooldown() => _abstractAttack ? _abstractAttack.bCoolTime : false;
    public bool IsSkillCooldown() => _abstractSkill ? _abstractSkill.bCoolTime : false;
    public bool IsAttackRange() => _abstractAttack ? _abstractAttack.IsInRange() : false;
    public bool IsSkillRange() => _abstractSkill ? _abstractSkill.IsInRange() : false;
    public bool HasMoveTarget() => _abstractMove ? _abstractMove.bhasTarget() : false;
    public bool IsOnlyWalkOrIdleAnimationPlaying()
    {
        var currentAnimation = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
        return currentAnimation == Animator.StringToHash("walk") || currentAnimation == Animator.StringToHash("idle");
    }

   

}
