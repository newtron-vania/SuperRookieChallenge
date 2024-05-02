using System;
using UnityEngine;

public class SimpleDamageSkill : AbstractSkill
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
        
        Array.Sort(targets, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        BaseCharacter target = targets[0].transform.GetComponent<BaseCharacter>();
        target.Hurt(_damage * _stat.Damage);
        target.SetBuff(_effect);
        
        Vector3 scale = _character.transform.localScale;
        if (_character.transform.position.x < target.transform.position.x)
        {
            _character.transform.localScale = new Vector3(-1 * Mathf.Abs(scale.x), scale.y, scale.z);
        }
        else
        {
            _character.transform.localScale = new Vector3(1 * Mathf.Abs(scale.x), scale.y, scale.z);
        }

        ResetCooltime();
        
        return true;
    }
}