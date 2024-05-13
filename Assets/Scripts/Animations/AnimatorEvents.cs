using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public BaseCharacter characterController;

    public void DeathEnd()
    {
        if (characterController != null)
        {
            characterController.AnimationDeadEnd();
        }
    }
}