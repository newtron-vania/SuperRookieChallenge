using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : IEffect
{
    public override Define.EEffectName _effectID
    {
        get { return Define.EEffectName.EEN_Stun; }
    }

    public override void SetBuff(Stat stat)
    {
        _duration = _maxDuration;
        stat.Accelerate = 0f;
    }

    public override void RemoveBuff(Stat stat)
    {
        _duration = 0f;
        stat.Accelerate = 1f;
    }
}
