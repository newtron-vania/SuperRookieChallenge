using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacter : MonoBehaviour
{
    public Define.ECharacterType _id = Define.ECharacterType.ECT_Empty;

    private BTMachine _btMachine;

    private AbstractAttack _abstractAttack;
    private AbstractSkill _abstractSkill;
    private AbstractMove _abstractMove;

    private Stat _stat;
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private Transform target;
    
    private void Awake()
    {
        _abstractAttack = GetComponent<AbstractAttack>();
        _abstractSkill = GetComponent<AbstractSkill>();
        _abstractMove = GetComponent<AbstractMove>();
        _stat = GetComponent<Stat>();

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
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
        
    }

    public void UseSkill()
    {
        
    }
    //이동 이벤트
    public void Move()
    {
        _abstractMove.Move();
    }

    public void Victory()
    {
        _animator.Play("victory");
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
        _animator.Play("idle");
    }
    
    public void Dead()
    {
        
    }
    
    public void Revive()
    {
        
    }
    
    public bool IsDead() => _stat.Hp <= 0;
    public bool IsAnimationPlaying(string animName) => _animator.GetCurrentAnimatorStateInfo(0).IsName(animName);
    public bool IsAttackCooldown() => _abstractAttack.bCoolTime;
    public bool IsSkillCooldown() => _abstractSkill.bCoolTime;
    public bool IsAttackRange() => _abstractAttack.IsInRange();
    public bool IsSkillRange() => _abstractSkill.IsInRange();


}
