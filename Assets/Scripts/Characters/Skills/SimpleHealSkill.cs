using System;
using UnityEngine;

public class SimpleHealSkill : AbstractSkill
{
    [SerializeField] 
    private RaycastHit2D[] targets; 
    
    public override bool IsInRange()
    {
        Vector3 myPos = transform.position;
        targets = Physics2D.CircleCastAll(myPos, 0f, Vector2.up, LayerMask.GetMask("player"));

        if (targets.Length <= 0)
        {
            return false;
        }

        return true;
    }

    public override bool UseSkill()
    {
        if (!IsInRange())
        {
            return false;
        }
        
        Array.Sort(targets, (hit1, hit2) =>
        {
            var stat1 = hit1.transform.GetComponent<Stat>();
            var stat2 = hit2.transform.GetComponent<Stat>();
            return stat1.Hp.CompareTo(stat2.Hp);
        });

        BaseCharacter target = targets[0].transform.GetComponent<BaseCharacter>();
        
        target.Hurt(-1 * _damage * _stat.Damage);
        ResetCooltime();
        
        return true;
    }
}