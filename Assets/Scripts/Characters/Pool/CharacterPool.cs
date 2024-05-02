using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    [SerializeField] 
    private Transform[] _spawnPoint;

    [SerializeField] 
    private string[] _spawnCharacterName;
    
    [SerializeField]
    private float _spawnTime = 5f;
    
    private List<BaseCharacter> _characterList = new List<BaseCharacter>();
    private List<float> _characterReviveTime = new List<float>();
    private SimpleCharacterFactory _factory;

    public void Init()
    {
        _spawnPoint = GetComponentsInChildren<Transform>(false);
        _factory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Player);
    }

    private void Awake()
    {
        Init();
    }

    public void Start()
    {
        for (int i = 0; i < _spawnCharacterName.Length; i++)
        {
            _characterList.Add(_factory.Create(_spawnCharacterName[i]));
            _characterReviveTime.Add(0f);
            _characterList[i].transform.position = _spawnPoint[i].position;
        }
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

        _characterReviveTime[index] = _spawnTime;
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
