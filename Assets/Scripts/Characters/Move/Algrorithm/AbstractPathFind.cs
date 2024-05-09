using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPathFind
{
    public float _maxDistance = 50f;
    public float _stepDistance = 1f;
    protected Transform _character;

    public AbstractPathFind(float maxDistance, float stepDistance)
    {
        _maxDistance = maxDistance;
        _stepDistance = stepDistance;
    }
    public abstract List<Vector3> FindPath(Vector3 start, Vector3 target);

    public void SetCharacter(Transform character)
    {
        _character = character;
    }
}

