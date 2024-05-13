using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    [SerializeField] private string name;

    private void Start()
    {
        BaseCharacter character = new SimpleCharacterFactory(Define.ECharacterType.ECT_Player).Create(name);
        character.transform.position = new Vector3(0f, 0f, 0f);

        BaseCharacter monster = new SimpleCharacterFactory(Define.ECharacterType.ECT_Enemy).Create("Monster");
        monster.transform.position = new Vector3(5f, 5f, 5f);
    }
}
