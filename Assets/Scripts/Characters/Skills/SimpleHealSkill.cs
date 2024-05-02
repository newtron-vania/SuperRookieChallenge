using System;
using UnityEngine;

public class SimpleHealSkill : AbstractSkill
{
    [SerializeField] 
    private RaycastHit2D[] targets; 
    
    public override bool IsInRange()
    {
        Vector3 myPos = transform.position;
        targets = Physics2D.CircleCastAll(myPos, 0f, Vector2.up, SetTargetLayer());

        if (targets.Length <= 0)
        {
            return false;
        }

        return true;
    }

    private int SetTargetLayer()
    {
        // Create layer masks by specifying the layers you are interested in
        int monsterLayerMask = 1 << LayerMask.NameToLayer("monster");
        int playerLayerMask = 1 << LayerMask.NameToLayer("player");

        // Combine the masks using bitwise OR
        int combinedMask = monsterLayerMask | playerLayerMask;

        // Exclude this GameObject's layer using bitwise operations
        int finalMask = combinedMask & (1 << gameObject.layer);

        return finalMask;
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