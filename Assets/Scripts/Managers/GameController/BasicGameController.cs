using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGameController : IGameController
{
    private int _level = 1;

    private BasicGameData _data;
    
    private List<BaseCharacter> _characterList = new List<BaseCharacter>();
    
    public int Level
    {
        get { return _level;}
        set { _level = value; }
    }
    
    public BasicGameController(IGameData data)
    {
        SaveGameData(data);
    }

    public void RegisterCharacter(BaseCharacter character)
    {
        _characterList.Add(character);
    }
    
    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void EndGame() 
    {
        Time.timeScale = 0f;
    }

    public void SaveGameData(IGameData data)
    {
        _data = data as BasicGameData;
    }

    public IGameData LoadGameData()
    {
        return _data;
    }
}