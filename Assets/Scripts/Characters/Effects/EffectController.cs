using System;
using System.Collections.Generic;
using UnityEngine;


public class EffectController : MonoBehaviour
{
    private Stat _stat;
    private Dictionary<Define.EEffectName,IEffect> _effectDict = new Dictionary<Define.EEffectName,IEffect>();
    private List<IEffect> _removeList = new List<IEffect>();
    
    private void Start()
    {
        _stat = GetComponent<Stat>();
    }
    
    public void Add(IEffect effect)
    {
        _effectDict.Add(effect._effectID, effect);
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
        }
        _removeList.Clear();
    }
    

}