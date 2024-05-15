using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoint;

    [SerializeField] private string _spawnMonsterName = "Monster";

    [SerializeField] private float _spawnTime = 5f;

    [SerializeField] private int _killCount;

    [SerializeField] private int _bossSpawnCount = 5;

    private readonly HashSet<BaseCharacter> _spawnUnit = new();

    private SimpleCharacterFactory _bossFactory;

    private bool _bSpawning;

    private SimpleCharacterFactory _monsterFactory;

    private bool bBossSpawn;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (!_bSpawning)
            StartCoroutine(SpawnMonster());
        if (!bBossSpawn && _killCount >= _bossSpawnCount)
        {
            bBossSpawn = true;
            SpawnBoss();
        }
    }

    public void AddKillCount(int value)
    {
        _killCount += value;
    }

    public void Init()
    {
        _monsterFactory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Enemy);
        _bossFactory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Boss);
        _spawnPoint = GetComponentsInChildren<Transform>(false).Where(c => c.gameObject != gameObject).ToArray();
    }

    private void SpawnBoss()
    {
        BaseCharacter Boss = null;
        Boss = _bossFactory.Create(_spawnMonsterName);
        Boss.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        Boss.DeathActionEvent -= CheckBossDeath;
        Boss.DeathActionEvent += CheckBossDeath;
    }


    private IEnumerator SpawnMonster()
    {
        _bSpawning = true;
        var monster = _monsterFactory.Create(_spawnMonsterName);
        _spawnUnit.Add(monster);
        monster.DeathActionEvent -= CheckMonsterDeath;
        monster.DeathActionEvent += CheckMonsterDeath;
        monster.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        yield return new WaitForSeconds(_spawnTime);
        _bSpawning = false;
    }

    private void CheckMonsterDeath(BaseCharacter character)
    {
        _spawnUnit.Remove(character);
        Managers.Resource.Destroy(character.gameObject);
        AddKillCount(1);
    }

    private void CheckBossDeath(BaseCharacter character)
    {
        _killCount = 0; // Reset
        bBossSpawn = false;
        //게임 재시작
    }
}