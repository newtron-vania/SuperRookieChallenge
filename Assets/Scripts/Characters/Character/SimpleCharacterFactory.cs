using System;
using UnityEngine;

public class SimpleCharacterFactory
{
    private BaseCharacter _character;

    public void SetCharacter(BaseCharacter character)
    {
        _character = character;
    }

    BaseCharacter Create(string unitName)
    {
        BaseCharacter character = Managers.Resource.Instantiate($"Character/{unitName}").
            GetComponent<BaseCharacter>();
        if (character == null)
        {
            Debug.Log($"Cannot Find Character {unitName}");
        }
        return character;
    }
}