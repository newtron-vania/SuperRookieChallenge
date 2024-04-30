using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    protected int _damage = 0;

    public int Damage
    {
        set { _damage = value; }
    }
    public abstract bool Attack(List<BaseCharacter> targets);

    public abstract bool IsInRange();
    
    protected virtual void GiveDamage(BaseCharacter target)
    {
        target.GetComponent<Stat>().Hp -= _damage;
    }

}
