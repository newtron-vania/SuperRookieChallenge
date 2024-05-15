using UnityEngine;

public class AnimatorEvents : MonoBehaviour
{
    public BaseCharacter characterController;

    public void DeathEnd()
    {
        if (characterController != null) characterController.AnimationDeadEnd();
    }
}