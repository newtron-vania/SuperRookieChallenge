using System;
using UnityEngine;

public abstract class IEffect
{
    // 버프 지속시간
    protected float _duration;

    // 다음 효과
    private IEffect _effect;

    // 버프 최대 지속시간
    protected float _maxDuration = 1f;
    public abstract Define.EEffectName _effectID { get; }

    public float MaxDuration
    {
        set => _maxDuration = value;
    }

    public bool bEnd()
    {
        return _duration <= 0;
    }

    public void onUpdate()
    {
        _duration = Mathf.Max(_duration - Time.deltaTime, 0);
    }

    public void ApplyEffect(IEffect effect)
    {
        var newEffect = effect;
        if (newEffect != null) newEffect.ApplyEffect(_effect);
        _effect = newEffect;
    }

    public void SetDuration(float time)
    {
        _duration = MathF.Max(_duration, time);
    }

    public abstract void SetBuff(Stat stat);

    public abstract void RemoveBuff(Stat stat);
}