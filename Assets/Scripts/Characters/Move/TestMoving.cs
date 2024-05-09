using System;
using System.Collections.Generic;
using UnityEngine;
public class TestMoving : MonoBehaviour
{
    private AbstractPathFind _pathFind;

    public Transform _target;
    
    public float speed = 5f;  // 이동 속도
    private List<Vector3> path;  // 이동할 경로

    [SerializeField]
    private float _maxdistance = 50f;
    [SerializeField]
    private float _mindistance = 0.1f;
    private void Start()
    {
        _pathFind = new JPS(_maxdistance, _mindistance);
        _pathFind.SetCharacter(this.transform);
    }

    private void Update()
    {
        if (!_target)
        {
            FindTarget();
            return;
        }

        List<Vector3> nextPath = _pathFind.FindPath(this.transform.position, _target.position);
        if (nextPath.Count <= 0)
        {
            Debug.Log("Can't Find path!");
            return;
        }
        for(int i = 1; i < nextPath.Count; i++)
        {
            Debug.DrawLine(nextPath[i-1], nextPath[i], Color.red,Time.deltaTime);
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPath[0], speed * Time.deltaTime);
    }
    
    private void FindTarget()
    {
        Vector3 myPos = transform.position;
        RaycastHit2D[] targets = Physics2D.CircleCastAll(myPos, _maxdistance, Vector2.up, 0f, SetTargetLayer());

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