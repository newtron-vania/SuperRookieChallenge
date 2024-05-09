using System;
using UnityEngine;

public class SimpleMultiDamageSkill : AbstractSkill
{
    [SerializeField] 
    private RaycastHit2D[] targets; 

    public override bool IsInRange()
    {
        Vector3 myPos = transform.position;
        targets = Physics2D.CircleCastAll(myPos, _range, Vector2.up, 0f, SetTargetLayer());
        
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
        int finalMask = combinedMask & ~(1 << gameObject.layer);

        return finalMask;
    }
    public override bool UseSkill()
    {
        if (!IsInRange())
        {
            return false;
        }

        foreach (var VARIABLE in targets)
        {
            BaseCharacter target = VARIABLE.transform.GetComponent<BaseCharacter>();
            
            target.Hurt(_damage * _stat.Damage);
            target.SetBuff(_effect);
        }
        
        ResetCooltime();
        
        return true;
    }
}