using UnityEngine;

public abstract class IEffectScriptableObj : ScriptableObject
{
    public abstract IEffect GetEffect();
}