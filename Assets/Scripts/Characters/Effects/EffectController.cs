using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class EffectController : MonoBehaviour
{
    [SerializeField] [ReadOnly(true)] private List<Define.EEffectName> _effectList = new();

    private readonly Dictionary<Define.EEffectName, IEffect> _effectDict = new();
    private readonly List<IEffect> _removeList = new();
    private Stat _stat;

    private void Awake()

    {
        _stat = GetComponent<Stat>();
    }

    private void Update()
    {
        foreach (var effect in _effectDict)
        {
            effect.Value.onUpdate();
            if (effect.Value.bEnd()) _removeList.Add(effect.Value);
        }

        RemoveEffects();
    }

    public void Add(IEffect effect)
    {
        Debug.Log($"Effect {effect} Added!");
        if (_effectDict.ContainsKey(effect._effectID))
        {
            _effectDict[effect._effectID] = effect;
        }
        else
        {
            _effectDict.Add(effect._effectID, effect);
            _effectList.Add(effect._effectID);
        }
        effect.SetBuff(_stat);
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
        foreach (var effect in _effectDict) effect.Value.RemoveBuff(_stat);
        _effectDict.Clear();
    }
}