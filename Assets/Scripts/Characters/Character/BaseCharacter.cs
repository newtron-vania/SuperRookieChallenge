using System;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public Define.ECharacterType _id = Define.ECharacterType.ECT_Empty;

    private AbstractAttack _abstractAttack;
    private AbstractMove _abstractMove;
    private AbstractSkill _abstractSkill;
    private Animator _animator;

    private BTMachine _btMachine;

    private EffectController _effectController;
    private HPBar _hpBar;
    private Rigidbody2D _rigidbody;

    private Stat _stat;

    public Action HurtEvent;


    private void Awake()
    {
        _effectController = GetComponent<EffectController>();
        _stat = GetComponent<Stat>();
        _hpBar = gameObject.FindChild<HPBar>(null, true) ??
                 Managers.Resource
                     .Instantiate("UI/WorldObject/UI_HPBar", transform.position + new Vector3(0, 2f, 0), transform)
                     .GetComponent<HPBar>();
        _animator = GetComponentInChildren<Animator>(true);
        _rigidbody = GetComponent<Rigidbody2D>();

        InitStrategy();
    }

    private void Update()
    {
        _btMachine?.Operate();
    }

    public event Action<BaseCharacter> DeathActionEvent;

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
    public bool HasMoveTarget()
    {
        return _abstractMove.Move();
    }

    public void Move()
    {
        if (!IsAnimationPlaying("walk")) PlayAnimation("walk");
    }

    public void Victory()
    {
        PlayAnimation("victory");
    }

    public void Hurt(float damage)
    {
        _stat.Hp -= damage;
        HurtEvent?.Invoke();
        if (_stat.Hp <= 0) Dead();
    }

    public void Idle()
    {
        PlayAnimation("idle");
    }

    public void Dead()
    {
        PlayAnimation("die");
        _hpBar.DisableHpBar();
        _abstractMove.ClearTarget();
        _effectController.Clear();
    }

    public void AnimationDeadEnd()
    {
        DeathActionEvent?.Invoke(this);
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        _stat.Hp = _stat.MaxHp;
        gameObject.SetActive(true);
    }

    // 버프 및 디버프 처리
    public void SetBuff(IEffect effect)
    {
        if (effect == null) return;
        _effectController.Add(effect);
    }

    private void PlayAnimation(string name)
    {
        _animator.Play(name, -1, 0f);
    }

    public bool IsDead()
    {
        return _stat.Hp <= 0;
    }

    public bool IsAnimationPlaying(string animName)
    {
        var animInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.IsName(animName) && animInfo.normalizedTime >= 0 && animInfo.normalizedTime < 1f) return true;
        return false;
    }


    public bool IsAttackCooldown()
    {
        return _abstractAttack ? _abstractAttack.bCoolTime : false;
    }

    public bool IsSkillCooldown()
    {
        return _abstractSkill ? _abstractSkill.bCoolTime : false;
    }

    public bool IsAttackRange()
    {
        return _abstractAttack ? _abstractAttack.IsInRange() : false;
    }

    public bool IsSkillRange()
    {
        return _abstractSkill ? _abstractSkill.IsInRange() : false;
    }

    public bool IsOnlyWalkOrIdleAnimationPlaying()
    {
        var animInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return animInfo.IsName("walk") || animInfo.IsName("idle") || animInfo.normalizedTime >= 1f;
    }
}