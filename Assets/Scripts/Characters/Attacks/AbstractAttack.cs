using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
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
    public abstract bool Attack(Stat stat);

    public abstract bool IsInRange();

    public bool bCoolTime
    {
        get { return _currentCooltime > 0f; }
    }
    protected virtual void GiveDamage(Stat stat, BaseCharacter target)
    {
        target.GetComponent<Stat>().Hp -= _damage * stat.Damage;
    }

}
