using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//장애물이 있는 경로는 이동할 수 없는 MoveType 구현 클래스
public class Walk : MoveType
{

    public override bool Move(BaseCharacter target)
    {

        return true;
    }
    
}
