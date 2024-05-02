using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 이동 방식 정의 추상 인터페이스
public abstract class AbstractMove : MonoBehaviour
{
    protected Transform _target;
    protected BaseCharacter _character;
    protected Stat _stat;
    public void Init(BaseCharacter character, Stat stat)
    {
        _character = character;
        _stat = stat;
    }
    
    public abstract bool Move();

    protected bool bhasTarget()
    {
        return _target != null || _target.gameObject.activeSelf;
    }
}
