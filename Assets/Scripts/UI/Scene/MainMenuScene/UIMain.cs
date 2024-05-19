using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMain : UI_Scene
{
    enum Buttons
    {
        ButtonStart
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        
        GetButton((int)Buttons.ButtonStart).gameObject.AddUIEvent(ShowGameSettingUI);
    }


    private void ShowGameSettingUI(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UIGameSetting>();
    }
}
