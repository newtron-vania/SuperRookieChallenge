using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    protected BaseCharacter _character;
    protected Stat _stat;
    
    [SerializeField]
    protected int _damage = 1;
    
    [SerializeField]
    protected float _range = 0;
    
    [SerializeField] 
    private float _cooltime = 1.5f;

    [SerializeField, ReadOnly] 
    private float _currentCooltime = 0f;

    private List<BaseCharacter> targets;

    public int Damage
    {
        set { _damage = value; }
    }

    public void ResetCooltime()
    {
        _currentCooltime = _cooltime;
    }
    
    public bool bCoolTime
    {
        get { return _currentCooltime > 0f; }
    }
    
    public void Init(BaseCharacter character, Stat stat)
    {
        _character = character;
        _stat = stat;
    }
    
    private void Update()
    {
        if (_currentCooltime > 0)
        {
            _currentCooltime = Mathf.Max(_currentCooltime - Time.deltaTime * _stat.Accelerate, 0);
        }
    }
    
    public abstract bool Attack();

    public abstract bool IsInRange();

    protected virtual void GiveDamage(Stat stat, BaseCharacter target)
    {
        target.GetComponent<Stat>().Hp -= _damage * stat.Damage;
    }

    private void OnDisable()
    {
        _currentCooltime = 0f;
    }
}
