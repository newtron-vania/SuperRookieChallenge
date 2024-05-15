using UnityEngine;

// 이동 방식 정의 추상 인터페이스
public abstract class AbstractMove : MonoBehaviour
{
    protected BaseCharacter _character;
    protected Stat _stat;
    protected Transform _target;

    public void Init(BaseCharacter character, Stat stat)
    {
        _character = character;
        _stat = stat;
    }

    public abstract bool Move();

    public bool bhasTarget()
    {
        return _target != null && _target.gameObject.activeSelf;
    }

    public void ClearTarget()
    {
        _target = null;
    }
}