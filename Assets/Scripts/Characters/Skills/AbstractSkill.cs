using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSkill : MonoBehaviour
{
    [SerializeField]
    private int _range = 0;
    
    public int Range
    {
        get { return _range; }
    }
    public abstract bool IsInRange();

    public abstract bool UseSkill();
}
