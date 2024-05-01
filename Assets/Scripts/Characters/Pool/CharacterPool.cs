using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    private List<BaseCharacter> _characterList = new List<BaseCharacter>();
    private List<float> _characterReviveTime = new List<float>();
    private SimpleCharacterFactory _factory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Player);
    public void Init()
    {
        _characterList.Add(_factory.Create("Knight"));
        _characterReviveTime.Add(0f);
        _characterList.Add(_factory.Create("Peasant"));
        _characterReviveTime.Add(0f);
        _characterList.Add(_factory.Create("Priest"));
        _characterReviveTime.Add(0f);
        _characterList.Add(_factory.Create("Thief"));
        _characterReviveTime.Add(0f);
    }

    private void Revive()
    {
        
    }

    public bool GameOver()
    {
        bool isOver = true;
        foreach (var VARIABLE in _characterList)
        {
            if (!VARIABLE.IsDead())
            {
                isOver = false;
                break;
            }
        }
        return isOver;
    }
    void Update()
    {
        
    }
}
