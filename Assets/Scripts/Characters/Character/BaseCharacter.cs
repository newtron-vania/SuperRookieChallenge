using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacter : MonoBehaviour
{
    public Define.ECharacterType _id = Define.ECharacterType.ECTEmpty;

    private BTMachine _btMachine;


    void Init(Define.ECharacterType id)
    {
        _id = id;
        switch (id)
        {
            case Define.ECharacterType.ECTPlayer:
                break;
            case Define.ECharacterType.ECTEnemy:
                break;
            case Define.ECharacterType.ECTBoss:
                break;
        }
    }
    public void Revive()
    {
        
    }

    public void Dead()
    {
        
    }

    public void Attack()
    {
        
    }

    public void Move()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _btMachine?.Operate();
    }
}
