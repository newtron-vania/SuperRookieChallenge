using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public Transform[] _spawnPoint;
    
    [SerializeField]
    private string _spawnMonsterName = "Monster";

    private SimpleCharacterFactory _monsterFactory;
    private SimpleCharacterFactory _bossFactory;
    
    [SerializeField]
    private float _spawnTime = 5f;

    private bool _bSpawning = false;
    
    HashSet<BaseCharacter> _spawnUnit = new HashSet<BaseCharacter>();

    [SerializeField]
    int _maxSpawnUnit = 50;

    [ReadOnly]
    private int _killCount = 0;

    [SerializeField] 
    private int _bossSpawnCount = 5;

    private bool bBossSpawn = false;
    public void AddKillCount(int value) { _killCount += value; }
    private void Start()
    {
        _monsterFactory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Enemy);
        _bossFactory = new SimpleCharacterFactory(Define.ECharacterType.ECT_Boss);
        _spawnPoint = GetComponentsInChildren<Transform>();
        //Managers.Game._OnSpawnEvent -= AddKillCount;
        //Managers.Game._OnSpawnEvent += AddKillCount;
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

    void SpawnBoss()
    {
        BaseCharacter Boss = null;
        Boss = _bossFactory.Create(_spawnMonsterName);
        Boss.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        Boss.DeathActionEvent -= CheckBossDeath;
        Boss.DeathActionEvent += CheckBossDeath;

    }


    IEnumerator SpawnMonster()
    {
        _bSpawning = true;
        BaseCharacter monster = _monsterFactory.Create(_spawnMonsterName);
        _spawnUnit.Add(monster);
        monster.transform.position = _spawnPoint[Random.Range(1, _spawnPoint.Length)].position;
        yield return new WaitForSeconds(_spawnTime);
        _bSpawning = false;
    }
    
    private void CheckMonsterDeath(BaseCharacter character)
    {
        _spawnUnit.Remove(character);
        AddKillCount(1);
    }

    private void CheckBossDeath(BaseCharacter character)
    {
        _killCount = 0; // Reset
        bBossSpawn = false;
        //게임 재시작
    }
}
