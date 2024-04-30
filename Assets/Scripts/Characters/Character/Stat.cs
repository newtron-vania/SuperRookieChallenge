using Unity.VisualScripting;
using UnityEngine;

public class Stat : MonoBehaviour
{
    //레벨
    [SerializeField]
    private int _lv;
    //현재 hp
    [SerializeField]
    private float _hp;
    // 최대 hp
    [SerializeField]
    private float _maxHp;
    //기본 공격력
    [SerializeField]
    private float _damage;
    //기본 공격 사거리
    [SerializeField]
    private float _attackRange;
    //쿨타임 가속량
    [SerializeField]
    private float _accelerate;
    
    
    public int Level
    {
        get { return _lv; }
        set { _lv = value; }
    }
    
    public float Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            if (_hp > _maxHp)
            {
                _hp = _maxHp;
            }
        }
    }
    
    public float MaxHp
    {
        get { return _maxHp; }
        set { _maxHp = value; }
    }
    
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    
    public float AttackRange
    {
        get { return _attackRange; }
        set { _attackRange = value; }
    }
    
    public float Accelerate
    {
        get { return _accelerate; }
        set { _accelerate = value; }
    }
    
}
