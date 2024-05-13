using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StunObj", menuName = "Effects/StunObj")]
public class StunScriptableObj : IEffectScriptableObj
{ 
    IEffect Effect = new Stun();

    public int _maxDuration = 1;
    
    public override IEffect GetEffect()
    {
        Effect.MaxDuration = _maxDuration;
        return Effect;
    }
}
