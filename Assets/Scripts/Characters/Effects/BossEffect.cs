using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : IEffect
{
    private float _maxDuration = float.MaxValue;
    
    
    private float _hpUp = 2f;
    private float _damageUp = 1.5f;
    private float _sizeUp = 1.5f;
    public override Define.EEffectName _effectID
    {
        get { return Define.EEffectName.EEN_Boss; }
    }

    public override void SetBuff(Stat stat)
    {
        _duration = _maxDuration;
        stat.MaxHp *= _hpUp;
        stat.Hp = stat.MaxHp;
        stat.Damage *= _damageUp;
        stat.transform.localScale *= _sizeUp;
    }

    public override void RemoveBuff(Stat stat)
    {
        _duration = 0f;
        stat.MaxHp /= _hpUp;
        stat.Hp = Mathf.Min(stat.MaxHp, 0f);
        stat.Damage /= _damageUp;
        stat.transform.localScale /= _sizeUp;
    }
}
