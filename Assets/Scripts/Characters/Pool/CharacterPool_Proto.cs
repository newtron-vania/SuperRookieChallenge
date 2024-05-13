using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPool_Proto : MonoBehaviour
{
    
    [SerializeField] 
    private Transform[] _spawnPoint;

    [SerializeField] 
    private string[] _spawnCharacterName;
    
    [SerializeField]
    private float _spawnTime = 5f;
    
    private Dictionary<BaseCharacter, float> _characterReviveTime = new Dictionary<BaseCharacter, float>();
    private SimpleCharacterFactory _factory;
    
    private BasicGameController _gameController;

    public void Init()
    {
        _gameController = Managers.Game.GetGameController() as BasicGameController;
        _factory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Player);
        _spawnPoint = GetComponentsInChildren<Transform>(false).Where(c => c.gameObject != this.gameObject).ToArray();
    }

    private void Awake()
    {
        Init();
    }

    public void Start()
    {
        for (int i = 0; i < _spawnCharacterName.Length; i++)
        {
            BaseCharacter character = _factory.Create(_spawnCharacterName[i]);
            if (character)
            {
                character.DeathActionEvent -= CheckCharacterDeath;
                character.DeathActionEvent += CheckCharacterDeath;
                _gameController.RegisterCharacter(character);
                _characterReviveTime.Add(character, 0f);
                character.transform.position = _spawnPoint[i].position;
            }
        }
    }

    public bool GameOver()
    {
        bool isOver = true;
        foreach (var VARIABLE in _characterReviveTime)
        {
            if (VARIABLE.Value <= 0f)
            {
                isOver = false;
                break;
            }
        }
        return isOver;
    }

    private void CheckCharacterDeath(BaseCharacter character)
    {
        if (!_characterReviveTime.ContainsKey(character))
        {
            // Character not found in the list
            Debug.Log("Character not found in the list.");
            return;
        }
        
        if(GameOver()) Managers.Game.EndGame();

        _characterReviveTime[character] = _spawnTime;
    }
    void Update()
    {
        // for (int i = 0; i < _characterList.Count; i++)
        // {
        //     BaseCharacter character = _characterList[i];
        //     if (character.IsDead())
        //     {
        //         _characterReviveTime[i] = Mathf.Max(_characterReviveTime[i] - Time.deltaTime, 0);
        //         if (_characterReviveTime[i] <= 0)
        //         {
        //             character.Revive();
        //         }
        //     }
        // }
    }
}
