using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPool : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;

    [SerializeField] private string[] _spawnCharacterName;

    [SerializeField] private float _spawnTime = 5f;

    private readonly List<BaseCharacter> _characterList = new();
    [SerializeField]
    private List<float> _characterReviveTime = new();
    private SimpleCharacterFactory _factory;

    private void Awake()
    {
        Init();
    }

    public void Start()
    {
        for (var i = 0; i < _spawnCharacterName.Length; i++)
        {
            var character = _factory.Create(_spawnCharacterName[i]);
            if (character)
            {
                character.DeathActionEvent -= CheckCharacterDeath;
                character.DeathActionEvent += CheckCharacterDeath;
                _characterList.Add(character);
                _characterReviveTime.Add(0f);
                _characterList[i].transform.position = _spawnPoint[i].position;
            }
        }
    }

    private void Update()
    {
        for (var i = 0; i < _characterList.Count; i++)
        {
            var character = _characterList[i];
            if (_characterReviveTime[i] > 0)
            {
                _characterReviveTime[i] = Mathf.Max(_characterReviveTime[i] - Time.deltaTime, 0);
                if (_characterReviveTime[i] <= 0)
                {
                    character.Revive();
                    Debug.Log($"Character {character.name} Revive!");
                    character.transform.position = _spawnPoint[Random.Range(0, _spawnPoint.Length)].position;
                }
            }
        }
    }

    public void Init()
    {
        _factory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Player);
        _spawnPoint = GetComponentsInChildren<Transform>(false).Where(c => c.gameObject != gameObject).ToArray();
    }

    public bool GameOver()
    {
        var isOver = true;
        foreach (var VARIABLE in _characterList)
            if (!VARIABLE.IsDead())
            {
                isOver = false;
                break;
            }

        return isOver;
    }

    private void CheckCharacterDeath(BaseCharacter character)
    {
        var index = _characterList.IndexOf(character);
        if (index == -1)
        {
            // Character not found in the list
            Debug.Log("Character not found in the list.");
            return;
        }

        if (GameOver()) Managers.Game.EndGame();

        _characterReviveTime[index] = _spawnTime;
    }
}