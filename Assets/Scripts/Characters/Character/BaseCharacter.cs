using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterType
{
    ECTEmpty,
    ECTPlayer,
    ECTEnemy,
    ECTBoss
}

public class BaseCharacter : MonoBehaviour
{
    public ECharacterType id = ECharacterType.ECTEmpty;

    private BTMachine _btMachine;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        // _btMachine = new BTMachine();
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
