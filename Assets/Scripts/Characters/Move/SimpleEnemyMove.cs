using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//장애물이 있는 경로는 이동할 수 없는 MoveType 구현 클래스
public class SimpleEnemyMove : AbstractMove
{
    [SerializeField] 
    private float _range = 3;
    
    public override bool Move()
    {
        if (bhasTarget())
        {
            FindTarget();
            return false;
        }
        
        if ((_target.position - transform.position).magnitude < 0.1f)
        {
            Debug.Log($"Too Near with target : {(_target.position - transform.position).magnitude}");
            return false;
        }

        Vector3 dir = (_target.position - transform.position).normalized;
        transform.position += Time.deltaTime * _stat.Accelerate * dir;
        
        return true;
    }
    
    private void FindTarget()
    {
        Vector3 myPos = transform.position;
        RaycastHit2D[] targets = Physics2D.CircleCastAll(myPos, 0f, Vector2.up, SetTargetLayer());

        if (targets.Length <= 0)
        {
            _target = null;
            return;
        }
        
        Array.Sort(targets, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        _target = targets[0].transform;
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
    
}
