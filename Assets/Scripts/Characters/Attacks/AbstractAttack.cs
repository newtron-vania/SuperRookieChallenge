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
    private float _range = 0;
    
    [SerializeField] 
    private float _cooltime = 1.5f;

    [ReadOnly] 
    private float _currentCooltime = 0f;

    private List<BaseCharacter> targets;

    public int Damage
    {
        set { _damage = value; }
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
    public abstract bool Attack(Stat stat);

    public abstract bool IsInRange();

    protected virtual void GiveDamage(Stat stat, BaseCharacter target)
    {
        target.GetComponent<Stat>().Hp -= _damage * stat.Damage;
    }
}
