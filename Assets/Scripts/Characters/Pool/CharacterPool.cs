using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    private List<BaseCharacter> _characterList = new List<BaseCharacter>();
    private List<float> _characterReviveTime = new List<float>();
    private SimpleCharacterFactory _factory;
    public void Init()
    {
        _factory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Player);
        _characterList.Add(_factory.Create("Knight"));
        _characterReviveTime.Add(0f);
        _characterList[0].DeathActionEvent += CheckCharacterDeath;
        _characterList.Add(_factory.Create("Archer"));
        _characterReviveTime.Add(0f);
        _characterList[1].DeathActionEvent += CheckCharacterDeath;
        _characterList.Add(_factory.Create("Priest"));
        _characterReviveTime.Add(0f);
        _characterList[2].DeathActionEvent += CheckCharacterDeath;
        _characterList.Add(_factory.Create("Thief"));
        _characterReviveTime.Add(0f);
        _characterList[3].DeathActionEvent += CheckCharacterDeath;
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

    private void CheckCharacterDeath(BaseCharacter character)
    {
        int index = _characterList.IndexOf(character);
        if (index == -1)
        {
            // Character not found in the list
            Debug.Log("Character not found in the list.");
            return;
        }

        _characterReviveTime[index] = 5f;
    }
    void Update()
    {
        for (int i = 0; i < _characterList.Count; i++)
        {
            BaseCharacter character = _characterList[i];
            if (character.IsDead())
            {
                _characterReviveTime[i] = Mathf.Max(_characterReviveTime[i] - Time.deltaTime, 0);
                if (_characterReviveTime[i] <= 0)
                {
                    character.Revive();
                }
            }
        }
    }
}
