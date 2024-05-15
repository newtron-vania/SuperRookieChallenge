using System;
using UnityEngine;

public class SimpleCharacterMove : AbstractMove
{
    [SerializeField] private float _range = 9999f;

    public override bool Move()
    {
        if (!bhasTarget())
        {
            FindTarget();
            return false;
        }

        if ((_target.position - transform.position).magnitude < 0.1f) return false;

        var dir = (_target.position - _character.transform.position).normalized;
        _character.transform.position += Time.deltaTime * _stat.Accelerate * dir;

        var scale = _character.transform.localScale;
        if (_character.transform.position.x < _target.transform.position.x)
            _character.transform.localScale = new Vector3(-1 * Mathf.Abs(scale.x), scale.y, scale.z);
        else
            _character.transform.localScale = new Vector3(1 * Mathf.Abs(scale.x), scale.y, scale.z);

        return true;
    }

    private void FindTarget()
    {
        var myPos = transform.position;
        var targets = Physics2D.CircleCastAll(myPos, _range, Vector2.up, 0f, SetTargetLayer());

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
        var monsterLayerMask = 1 << LayerMask.NameToLayer("monster");
        var playerLayerMask = 1 << LayerMask.NameToLayer("player");

        // Combine the masks using bitwise OR
        var combinedMask = monsterLayerMask | playerLayerMask;

        // Exclude this GameObject's layer using bitwise operations
        var finalMask = combinedMask & ~(1 << gameObject.layer);

        return finalMask;
    }
}