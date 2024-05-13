using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public float GameTime { get; set; }

    private IGameController _gameController;

    public void SetGameController(IGameController controller)
    {
        _gameController = controller;
    }
    
    public IGameController GetGameController()
    {
        return _gameController;
    }

    public void StartGame()
    {
        _gameController.StartGame();
    }

    public void EndGame()
    {
        _gameController.EndGame();
    }

    public void SetGameData(IGameData data)
    {
        _gameController.SaveGameData(data);
    }

    public IGameData LoadGameData()
    {
        return _gameController.LoadGameData();
    }
}
