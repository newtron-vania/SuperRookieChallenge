public interface IGameController
{
    void StartGame();
    void VictoryGame();
    void EndGame();
    void SaveGameData(IGameData data);
    IGameData LoadGameData();

    public void Clear();
}