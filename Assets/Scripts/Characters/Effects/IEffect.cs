using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class IEffect : MonoBehaviour
{
    public abstract Define.EEffectName _effectID { get; }

    // 다음 효과
    private IEffect _effect;
    // 버프 지속시간
    protected float duration = 0;
    
    public bool bEnd()
    {
        return duration <= 0;
    }

    public void onUpdate()
    {
        duration = Mathf.Max(duration - Time.deltaTime, 0);
    }

    public void ApplyEffect(IEffect effect)
    {
        IEffect newEffect = effect;
        if (newEffect != null)
        {
            newEffect.ApplyEffect(_effect);
        }
        _effect = newEffect;
    }

    public void SetDuration(float time)
    {
        duration = MathF.Max(duration, time);
    }
    public abstract void SetBuff(Stat stat);

    public abstract void RemoveBuff(Stat stat);
}
