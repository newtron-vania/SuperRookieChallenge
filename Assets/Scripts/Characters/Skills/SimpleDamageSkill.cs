using System;
using UnityEngine;

public class SimpleDamageSkill : AbstractSkill
{
    [SerializeField] 
    private RaycastHit2D[] targets; 

    public override bool IsInRange()
    {
        Vector3 myPos = transform.position;
        targets = Physics2D.CircleCastAll(myPos, 0f, Vector2.up, LayerMask.GetMask("monster"));

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
        
        Array.Sort(targets, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        BaseCharacter target = targets[0].transform.GetComponent<BaseCharacter>();
        target.Hurt(_damage * _stat.Damage);

        ResetCooltime();
        
        return true;
    }
}