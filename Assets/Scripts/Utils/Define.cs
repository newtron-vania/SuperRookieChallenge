using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum ECharacterType
    {
        ECTEmpty,
        ECTPlayer,
        ECTEnemy,
        ECTBoss
    }

    public enum EEffectName
    {
        None,
        Stun,
        Bleed,
        Burned
    }

    public enum SceneType
    {
        Unknown,
        GameScene
    }

}
