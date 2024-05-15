using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPathFind
{
    protected Transform _character;
    public float _maxDistance = 50f;
    public float _stepDistance = 1f;

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