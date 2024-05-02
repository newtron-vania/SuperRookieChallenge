using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : IEffect
{
    private float _maxDuration = 1f;
    public override Define.EEffectName _effectID
    {
        get { return Define.EEffectName.EEN_Stun; }
    }

    public override void SetBuff(Stat stat)
    {
        duration = _maxDuration;
        stat.Accelerate = 0f;
    }

    public override void RemoveBuff(Stat stat)
    {
        duration = 0f;
        stat.Accelerate = 1f;
    }
}
