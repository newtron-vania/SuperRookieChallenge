using Unity.Collections;
using UnityEngine;

public abstract class AbstractSkill : MonoBehaviour
{
    [SerializeField] protected float _damage;

    [SerializeField] protected float _range;

    [SerializeField] private float _cooltime = 1f;

    [SerializeField] [ReadOnly] private float _currentCooltime;

    protected BaseCharacter _character;

    protected IEffect _effect;
    protected Stat _stat;

    public float Range => _range;

    public bool bCoolTime => _currentCooltime > 0f;

    private void Update()
    {
        if (_currentCooltime > 0) _currentCooltime = Mathf.Max(_currentCooltime - Time.deltaTime * _stat.Accelerate, 0);
    }

    private void OnDisable()
    {
        _currentCooltime = 0f;
    }

    public void ResetCooltime()
    {
        _currentCooltime = _cooltime;
    }

    public void Init(BaseCharacter character, Stat stat)
    {
        _character = character;
        _stat = stat;
        var effects = GetComponent<EffectStorage>()?._skillEffect;

        if (effects == null) return;

        foreach (var VARIABLE in effects)
        {
            if (_effect == null)
            {
                _effect = VARIABLE.GetEffect();
                continue;
            }

            _effect.ApplyEffect(VARIABLE.GetEffect());
        }
    }

    public abstract bool IsInRange();
    public abstract bool UseSkill();
}