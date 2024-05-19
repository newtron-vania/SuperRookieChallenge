using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIVictory : UI_Popup
{
    enum Texts
    {
        LevelValueText
    }

    enum Buttons
    {
        ButtonBackToMain,
        ButtonNextLevel,
    }
    public override Define.PopupUIGroup PopupID
    {
        get { return Define.PopupUIGroup.UIVictory; }
    }
    
    public override void Init()
    {
        base.Init();
        
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        BasicGameController controller = Managers.Game.GetGameController() as BasicGameController;
        GetText((int)Texts.LevelValueText).text = controller.level.ToString("D2");
        GetButton((int)Buttons.ButtonBackToMain).gameObject.AddUIEvent(GoToMain);
        GetButton((int)Buttons.ButtonNextLevel).gameObject.AddUIEvent(ButtonMoveToNextLevel);
    }

    private void GoToMain(PointerEventData data)
    {
        Managers.Game.Clear();
        Managers.Scene.LoadScene(Define.SceneType.MainScene);
    }

    private void ButtonMoveToNextLevel(PointerEventData data)
    {
        BasicGameController controller = Managers.Game.GetGameController() as BasicGameController;
        controller.level += 1;
        Debug.Log($"GameManager Level : {(Managers.Game.GetGameController() as BasicGameController).level}");
        Managers.Scene.LoadScene(Define.SceneType.GameScene);
    }

}
