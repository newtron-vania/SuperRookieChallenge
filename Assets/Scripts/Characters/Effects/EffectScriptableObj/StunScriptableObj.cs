using UnityEngine;

[CreateAssetMenu(fileName = "StunObj", menuName = "Effects/StunObj")]
public class StunScriptableObj : IEffectScriptableObj
{
    public int _maxDuration = 1;
    private readonly IEffect Effect = new Stun();

    public override IEffect GetEffect()
    {
        Effect.MaxDuration = _maxDuration;
        return Effect;
    }
}