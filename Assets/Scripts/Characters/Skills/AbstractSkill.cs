using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public abstract class AbstractSkill : MonoBehaviour
{
    private IEffect _effect;
    
    [SerializeField] 
    protected float _damage;
    
    [SerializeField]
    private float _range = 0;

    [SerializeField] 
    private float _cooltime = 1f;

    [ReadOnly] 
    private float _currentCooltime = 0f;

    private void Awake()
    {
        IEffect[] effects = GetComponentsInChildren<IEffect>();
        foreach (IEffect effect in effects)
        {
            _effect.ApplyEffect(effect);
        }
    }

    public float Range
    {
        get { return _range; }
    }
    public abstract bool IsInRange();

    public abstract bool UseSkill();
}
