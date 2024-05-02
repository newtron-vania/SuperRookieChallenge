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
            _target = null;
            return false;
        }

        Vector3 dir = (_target.position - transform.position).normalized;
        transform.position += dir * Time.deltaTime * _stat.Accelerate;
        
        return true;
    }
    
}
