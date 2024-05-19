public class Define
{
    public enum ECharacterType
    {
        ECT_Empty,
        ECT_Player,
        ECT_Enemy,
        ECT_Boss
    }

    public enum EPlayerCharacterType
    {
        Archer,
        Priest,
        Thief,
        Knight
    }

    public enum EEffectName
    {
        EEN_None,
        EEN_Stun,
        EEN_Bleed,
        EEN_Burned,
        EEN_Boss
    }

    public enum PopupUIGroup
    {
        Unknown,
        UIGameSetting,
        UIVictory,
        UIGameOver,
    }

    public enum SceneType
    {
        Unknown,
        GameScene,
        MainScene
    }

    public enum SceneUI
    {
        Unknown,
        UIMain,
        UIGameScene,
    }

    public enum UIEvent
    {
        Click,
        Drag
    }
}