using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectStorage : MonoBehaviour
{
    [SerializeField]
    public List<IEffectScriptableObj> _attackEffect;
    [SerializeField]
    public List<IEffectScriptableObj> _skillEffect;
}
