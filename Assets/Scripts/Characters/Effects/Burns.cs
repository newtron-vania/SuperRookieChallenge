using UnityEngine;

public class Burns : IEffect
{
    public float _damange = 10;

    public float _initalDotTime = 0.2f;

    private float _currentTime = 0f;
    public override Define.EEffectName _effectID => Define.EEffectName.EEN_Burns;

    private Stat _targetStat;

    public override void SetBuff(Stat stat)
    {
        _duration = _maxDuration;
        _targetStat = stat;
    }

    public override void onUpdate()
    {
        base.onUpdate();
        _currentTime = Mathf.Max(_currentTime -= Time.deltaTime, 0f);
        if (_currentTime <= 0f)
        {
            _currentTime = _initalDotTime;
            _targetStat.Hp -= _damange * _initalDotTime;
        }
    }

    public override void RemoveBuff(Stat stat)
    {
        _duration = 0f;
    }
}