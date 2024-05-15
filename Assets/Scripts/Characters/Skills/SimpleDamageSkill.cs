using System;
using UnityEngine;

public class SimpleDamageSkill : AbstractSkill
{
    [SerializeField] private RaycastHit2D[] targets;

    public override bool IsInRange()
    {
        var myPos = transform.position;
        targets = Physics2D.CircleCastAll(myPos, _range, Vector2.up, 0f, SetTargetLayer());

        if (targets.Length <= 0) return false;

        var deadCount = 0;
        foreach (var target in targets) deadCount += target.transform.GetComponent<BaseCharacter>().IsDead() ? 1 : 0;

        if (targets.Length == deadCount) return false;

        return true;
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

    public override bool UseSkill()
    {
        if (!IsInRange()) return false;

        Array.Sort(targets, (hit1, hit2) => hit1.distance.CompareTo(hit2.distance));

        var target = targets[0].transform.GetComponent<BaseCharacter>();
        target.Hurt(_damage * _stat.Damage);
        target.SetBuff(_effect);

        var scale = _character.transform.localScale;
        if (_character.transform.position.x < target.transform.position.x)
            _character.transform.localScale = new Vector3(-1 * Mathf.Abs(scale.x), scale.y, scale.z);
        else
            _character.transform.localScale = new Vector3(1 * Mathf.Abs(scale.x), scale.y, scale.z);

        ResetCooltime();

        return true;
    }
}