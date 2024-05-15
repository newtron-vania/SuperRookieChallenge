public class GameManager
{
    private IGameController _gameController;
    public float GameTime { get; set; }

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