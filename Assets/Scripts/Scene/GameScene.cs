using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public override Define.SceneType _sceneType
    {
        get { return Define.SceneType.GameScene; }
    }

    private string _spawningPoolpath = "SpawningPool/";
    [SerializeField] private string _playerSpawningPoolName = "PlayerSpawningPool";
    [SerializeField] private string _monsterSpawningPoolName = "MonsterSpawningPool";
    protected override void Init()
    {
        base.Init();
        Managers.Game.StartGame();
        Managers.UI.ShowSceneUI<UIGameScene>();
        Managers.Resource.Instantiate(_spawningPoolpath + _playerSpawningPoolName);
        Managers.Resource.Instantiate(_spawningPoolpath + _monsterSpawningPoolName);
    }

    public override void Clear()
    {
        Managers.UI.Clear();
    }
}
