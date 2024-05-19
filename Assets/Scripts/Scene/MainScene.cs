using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    public override Define.SceneType _sceneType
    {
        get { return Define.SceneType.MainScene; }
    }

    protected override void Init()
    {
        base.Init();
        Managers.Game.StartGame();
        Managers.UI.ShowSceneUI<UIMain>();
    }

    public override void Clear()
    {
        Managers.UI.Clear();
    }
}
