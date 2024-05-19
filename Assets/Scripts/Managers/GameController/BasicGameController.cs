using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicGameController : IGameController
{
    public Action EventNextLevel;
    public List<BaseCharacter> _characters = new();
    public HashSet<BaseCharacter> _monsters = new();

    public int level = 1;

    private BasicGameData _data;

    public BasicGameController(IGameData data)
    {
        SaveGameData(data);
    }

    public int Level { get; set; } = 1;

    public void StartGame()
    {
        Time.timeScale = 1f;
        Managers.UI.CloseAllPopupUI();
    }

    public void VictoryGame()
    {
        Time.timeScale = 0f;
        InitAllCharacter();
        foreach (var VARIABLE in _characters)
        {
            VARIABLE.Victory();
        }

        foreach (var VARIABLE in _monsters)
        {
            VARIABLE.Dead();
        }
        
        _monsters.Clear();
        Managers.UI.ShowPopupUI<UIVictory>();
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        Managers.UI.ShowPopupUI<UIGameOver>();
    }

    public void SaveGameData(IGameData data)
    {
        _data = data as BasicGameData;
    }

    public IGameData LoadGameData()
    {
        return _data;
    }
    
    public void RegisterCharacter(BaseCharacter character)
    {
        _characters.Add(character);
    }

    public void InitAllCharacter()
    {
        foreach (var VARIABLE in _characters)
        {
            VARIABLE.Revive();
        }
    }

    public void Clear()
    {
        level = 1;
        foreach (var VARIABLE in _characters)
        {
            SceneManager.MoveGameObjectToScene(VARIABLE.gameObject, SceneManager.GetActiveScene());
        }
        _characters.Clear();
    }

}