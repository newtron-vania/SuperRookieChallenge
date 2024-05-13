using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class AbstractSkill : MonoBehaviour
{
    protected BaseCharacter _character;
    protected Stat _stat;
    
    protected IEffect _effect;

    [SerializeField] 
    protected float _damage = 0;
    
    [SerializeField]
    protected float _range = 0;

    [SerializeField] 
    private float _cooltime = 1f;
    
    [SerializeField, ReadOnly] 
    private float _currentCooltime = 0f;

    private void Update()
    {
        if (_currentCooltime > 0)
        {
            _currentCooltime = Mathf.Max(_currentCooltime - Time.deltaTime * _stat.Accelerate, 0);
        }
    }

    public void ResetCooltime()
    {
        _currentCooltime = _cooltime;
    }
    public void Init(BaseCharacter character, Stat stat)
    {
        _character = character;
        _stat = stat;
        List<IEffectScriptableObj> effects = GetComponent<EffectStorage>()?._skillEffect;

        if (effects == null)
        {
            return;
        }
        
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
    
    public float Range
    {
        get { return _range; }
    }
    public abstract bool IsInRange();

    public bool bCoolTime
    {
        get { return _currentCooltime > 0f; }
    }
    public abstract bool UseSkill();
    
    private void OnDisable()
    {
        _currentCooltime = 0f;
    }
}
