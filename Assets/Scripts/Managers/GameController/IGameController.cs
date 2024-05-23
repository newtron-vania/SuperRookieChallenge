public interface IGameController
{
    void StartGame(); // 게임 시작
    void VictoryGame(); // 게임 승리 로직
    void EndGame(); // 게임 종료 로직
    void SaveGameData(IGameData data); // 게임 데이터 저장하
    IGameData LoadGameData(); // 게임 데이터 불러오기

    public void Clear(); // 게임의 상태를 초기화
}