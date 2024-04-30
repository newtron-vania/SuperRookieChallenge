using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eEffectName
{
    None,
    Stun,
    Bleed,
    Burned
}


public abstract class IEffect : MonoBehaviour
{
    // 버프 지속시간
    protected float duration = 0;

    public abstract void SetBuff(Stat stat);

    public abstract void RemoveBuff(Stat stat);
}
