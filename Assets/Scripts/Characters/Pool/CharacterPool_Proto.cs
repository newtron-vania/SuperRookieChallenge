using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPool_Proto : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;

    [SerializeField] private string[] _spawnCharacterName;

    [SerializeField] private float _spawnTime = 5f;

    private readonly Dictionary<BaseCharacter, float> _characterReviveTime = new();
    private SimpleCharacterFactory _factory;

    private BasicGameController _gameController;

    private void Awake()
    {
        Init();
    }

    public void Start()
    {
        for (var i = 0; i < _spawnCharacterName.Length; i++)
        {
            BaseCharacter character = null;
            if (_gameController._characters.Count() == _spawnCharacterName.Length)
            {
                character = _gameController._characters[i];
                if (character)
                {
                    character.DeathActionEvent -= CheckCharacterDeath;
                    character.DeathActionEvent += CheckCharacterDeath;
                    _characterReviveTime.Add(character, 0f);
                    character.transform.position = _spawnPoint[i].position;
                }
            }
            else
            {
                character = _factory.Create(_spawnCharacterName[i]);
                //파괴되지 않도록 설정
                DontDestroyOnLoad(character);
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
    }

    private void Update()
    {
        foreach (var character in _gameController._characters)
        {
            if (character.IsDead())
            {
                _characterReviveTime[character] = Mathf.Max(_characterReviveTime[character] - Time.deltaTime, 0);
                if (_characterReviveTime[character] <= 0)
                {
                    character.Revive();
                    character.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;
                }
            }
        }
    }

    public void Init()
    {
        _gameController = Managers.Game.GetGameController() as BasicGameController;
        _factory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Player);
        _spawnPoint = GetComponentsInChildren<Transform>(false).Where(c => c.gameObject != gameObject).ToArray();
        _spawnCharacterName = (_gameController.LoadGameData() as BasicGameData).Characters;
    }

    public bool GameOver()
    {
        var isOver = true;
        foreach (var VARIABLE in _characterReviveTime)
            if (VARIABLE.Value <= 0f)
            {
                isOver = false;
                break;
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

        if (GameOver()) Managers.Game.EndGame();

        _characterReviveTime[character] = _spawnTime;
    }
}