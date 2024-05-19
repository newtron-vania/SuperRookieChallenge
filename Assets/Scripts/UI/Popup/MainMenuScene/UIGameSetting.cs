using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIGameSetting : UI_Popup
{
    private BasicGameData data;
    private TMP_Dropdown[] dropdowns;
    [SerializeField] private string[] _spawnCharacterName;
    public override Define.PopupUIGroup PopupID
    {
        get { return Define.PopupUIGroup.UIGameSetting; }
    }

    [SerializeField] private string[] characterList;
    enum Buttons
    {
        ButtonStart
    }
    
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.ButtonStart).gameObject.AddUIEvent(MoveToGameScene);
        
        List<string> PlayerCharacterList = new List<string>();
        foreach (var VARIABLE in Enum.GetValues(typeof(Define.EPlayerCharacterType)))
        {
            PlayerCharacterList.Add(VARIABLE.ToString());
        }

        dropdowns = gameObject.GetComponentsInChildren<TMP_Dropdown>(true);

        Debug.Log($"Dropdown count : {dropdowns.Length}");
        for (int i = 0; i < dropdowns.Length; i++)
        {
            dropdowns[i].ClearOptions();
            int index = i;
            SetCharacterName(dropdowns[index], PlayerCharacterList);
            SetDropdownValue(dropdowns[index], i);
            dropdowns[index].onValueChanged.AddListener(delegate { FunctionDropdown(index); });
        }

        SetGameManager();
    }
    
    private void SetGameManager()
    {
        data = new BasicGameData();
        data.Characters = new string[dropdowns.Length];
        for (int i = 0; i < data.Characters.Length; i++)
        {
            data.Characters[i] = dropdowns[i].options[dropdowns[i].value].text;
            Debug.Log("Updated BasicGameData[" + i + "] to " + data.Characters[i]);
        }

        characterList = data.Characters;
        BasicGameController controller = new BasicGameController(data);
        Managers.Game.SetGameController(controller);
    }

    private void SetCharacterName(TMP_Dropdown dropdown, List<string> PCharacterList)
    {
        dropdown.AddOptions(PCharacterList);
        dropdown.RefreshShownValue();
    }

    private void SetDropdownValue(TMP_Dropdown dropdown, int value)
    {
        dropdown.value = value;
        dropdown.RefreshShownValue();
    }
    private void FunctionDropdown(int index)
    {
        string op = dropdowns[index].options[dropdowns[index].value].text;
        data.Characters[index] = op;
        Debug.Log("Updated BasicGameData[" + index + "] to " + data.Characters[index]);
    }
    
    private void MoveToGameScene(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.SceneType.GameScene);
    }

    private void BackToUIMain(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
    }
    
}
