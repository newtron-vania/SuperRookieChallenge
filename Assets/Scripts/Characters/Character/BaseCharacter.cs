using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacter : MonoBehaviour
{
    public Define.ECharacterType _id = Define.ECharacterType.ECTEmpty;

    private BTMachine _btMachine;

    private AbstractAttack _abstractAttack;
    private AbstractSkill _abstractSkill;
    private AbstractMove _abstractMove;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _abstractAttack = GetComponent<AbstractAttack>();
        _abstractSkill = GetComponent<AbstractSkill>();
        _abstractMove = GetComponent<AbstractMove>();

        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Init(Define.ECharacterType id)
    {
        _id = id;
        switch (id)
        {
            case Define.ECharacterType.ECTPlayer:
                break;
            case Define.ECharacterType.ECTEnemy:
                
            case Define.ECharacterType.ECTBoss:
                break;
        }
    }
    public void Revive()
    {
        
    }

    public void Dead()
    {
        
    }

    public void Attack()
    {
        
    }

    public void Move()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _btMachine?.Operate();
    }
}
