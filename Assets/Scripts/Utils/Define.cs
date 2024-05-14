using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum ECharacterType
    {
        ECT_Empty,
        ECT_Player,
        ECT_Enemy,
        ECT_Boss
    }

    public enum EEffectName
    {
        EEN_None,
        EEN_Stun,
        EEN_Bleed,
        EEN_Burned,
        EEN_Boss,
    }
    
    public enum PopupUIGroup
    {
        Unknown,
    }

    public enum SceneUI
    {
        Unknown,
    }
    
    public enum UIEvent
    {
        Click,
        Drag,

    }

    public enum SceneType
    {
        Unknown,
        GameScene
    }

}
