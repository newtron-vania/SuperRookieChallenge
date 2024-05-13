using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class EffectController : MonoBehaviour
{
    private Stat _stat;
    private Dictionary<Define.EEffectName,IEffect> _effectDict = new Dictionary<Define.EEffectName,IEffect>();
    [SerializeField, ReadOnly(true)]
    private List<Define.EEffectName> _effectList = new List<Define.EEffectName>();
    private List<IEffect> _removeList = new List<IEffect>();
    
    private void Awake()
    {
        _stat = GetComponent<Stat>();
    }
    
    public void Add(IEffect effect)
    {
        Debug.Log($"Effect {effect} Added!");
        _effectDict.Add(effect._effectID, effect);
        _effectList.Add(effect._effectID);
        effect.SetBuff(_stat);
    }

    private void Update()
    {
        foreach (var effect in _effectDict)
        {
            effect.Value.onUpdate();
            if (effect.Value.bEnd())
            {
                _removeList.Add(effect.Value);
            }
        }
        
        RemoveEffects();
    }

    private void RemoveEffects()
    {
        foreach (var effect in _removeList)
        {
            effect.RemoveBuff(_stat);
            _effectDict.Remove(effect._effectID);
            _effectList.Remove(effect._effectID);
        }
        _removeList.Clear();
    }

    public void Clear()
    {
        foreach (var effect in _effectDict)
        {
            effect.Value.RemoveBuff(_stat);
        }
        _effectDict.Clear();
    }
    

}