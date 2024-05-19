using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScene : UI_Scene
{
    enum Texts
    {
        LevelValueText
    }
    public override void Init()
    {
        base.Init();
        Bind<TextMeshProUGUI>(typeof(Texts));

        BasicGameController controller = Managers.Game.GetGameController() as BasicGameController;
        Debug.Log($"gameController is {controller is null}");
        GetText((int)Texts.LevelValueText).text = controller.level.ToString("D2");
    }
}
