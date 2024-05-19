using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGameOver : UI_Popup
{
    enum Texts
    {
        LevelValueText
    }

    enum Buttons
    {
        ButtonBackToMain,
    }
    public override Define.PopupUIGroup PopupID
    {
        get { return Define.PopupUIGroup.UIGameOver; }
    }

    public override void Init()
    {
        base.Init();
        
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        BasicGameController controller = Managers.Game.GetGameController() as BasicGameController;
        GetText((int)Texts.LevelValueText).text = controller.level.ToString("D2");
        GetButton((int)Buttons.ButtonBackToMain).gameObject.AddUIEvent(GoToMain);
    }

    private void GoToMain(PointerEventData data)
    {
        Managers.Game.Clear();
        Managers.Scene.LoadScene(Define.SceneType.MainScene);
    }
}
