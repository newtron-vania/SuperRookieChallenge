using System.Collections.Generic;
using UnityEngine;

public class BasicGameController : IGameController
{
    private readonly List<BaseCharacter> _characterList = new();

    private BasicGameData _data;

    public BasicGameController(IGameData data)
    {
        SaveGameData(data);
    }

    public int Level { get; set; } = 1;

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

    public void RegisterCharacter(BaseCharacter character)
    {
        _characterList.Add(character);
    }
}