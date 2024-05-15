using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    [SerializeField] protected int _damage = 1;

    [SerializeField] protected float _range;

    [SerializeField] private float _cooltime = 1.5f;

    [SerializeField] [ReadOnly] private float _currentCooltime;

    protected BaseCharacter _character;

    [SerializeField] protected IEffect _effect;

    protected Stat _stat;

    private List<BaseCharacter> targets;

    public int Damage
    {
        set => _damage = value;
    }

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

        var effects = GetComponent<EffectStorage>()?._attackEffect;

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

    public abstract bool Attack();

    public abstract bool IsInRange();

    protected virtual void GiveDamage(Stat stat, BaseCharacter target)
    {
        target.GetComponent<Stat>().Hp -= _damage * stat.Damage;
    }
}