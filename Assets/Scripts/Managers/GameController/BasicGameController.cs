using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicGameController : IGameController
{
    public Action EventNextLevel; // 다음 레벨로 넘어가는 이벤트를 처리하는 액션

    public List<BaseCharacter> _characters = new(); // 게임 내 캐릭터 객체 리스트
    public HashSet<BaseCharacter> _monsters = new(); // 게임 내 몬스터 객체 집합

    public int level = 1; // 현재 게임 레벨

    private BasicGameData _data; // 게임 데이터 저장을 위한 변수

    public BasicGameController(IGameData data) // 생성자에서 게임 데이터를 초기화
    {
        SaveGameData(data);
    }

    public int Level { get; set; } = 1; // 게임 레벨 속성

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