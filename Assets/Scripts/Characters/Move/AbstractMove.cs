using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 이동 방식 정의 추상 인터페이스
public abstract class AbstractMove : MonoBehaviour
{
    private Transform _target;
    private void Start()
    {
        
    }
    // 다음 이동 좌표 반환 및 이동 성공 여부 확인
    public abstract bool Move();
}
