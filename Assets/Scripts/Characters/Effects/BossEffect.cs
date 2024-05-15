using UnityEngine;

public class BossEffect : IEffect
{
    private readonly float _damageUp = 1.5f;


    private readonly float _hpUp = 2f;
    private readonly float _maxDuration = float.MaxValue;
    private readonly float _sizeUp = 1.5f;

    public override Define.EEffectName _effectID => Define.EEffectName.EEN_Boss;

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