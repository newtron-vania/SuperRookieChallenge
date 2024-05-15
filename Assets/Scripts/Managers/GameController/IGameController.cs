public interface IGameController
{
    void StartGame();
    void EndGame();
    void SaveGameData(IGameData data);
    IGameData LoadGameData();
}