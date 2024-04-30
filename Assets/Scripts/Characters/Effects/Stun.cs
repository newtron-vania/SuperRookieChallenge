using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : IEffect
{
    public override Define.EEffectName _effectID
    {
        get { return Define.EEffectName.Stun; }
    }

    public override void SetBuff(Stat stat)
    {
        duration = 1f;
        stat.Accelerate = 0f;
    }

    public override void RemoveBuff(Stat stat)
    {
        duration = 0f;
        stat.Accelerate = 1f;
    }
}
