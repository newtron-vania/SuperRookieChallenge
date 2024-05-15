public class Stun : IEffect
{
    public override Define.EEffectName _effectID => Define.EEffectName.EEN_Stun;

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