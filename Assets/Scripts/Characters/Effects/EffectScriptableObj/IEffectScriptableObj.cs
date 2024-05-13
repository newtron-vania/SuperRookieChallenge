using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IEffectScriptableObj : ScriptableObject
{
    public abstract IEffect GetEffect();
}
