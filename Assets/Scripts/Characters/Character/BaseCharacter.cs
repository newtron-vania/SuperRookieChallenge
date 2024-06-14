using System;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public Define.ECharacterType _id = Define.ECharacterType.ECT_Empty;

    // 피해 관련 이벤트
    public Action HurtEvent;
    
    // 전략 패턴 적용
    private AbstractAttack _abstractAttack;
    private AbstractMove _abstractMove;
    private AbstractSkill _abstractSkill;
    
    // 캐릭터 스텟
    private Stat _stat;
    
    // 캐릭터 별 행동 제어 트리
    private BTMachine _btMachine;
    
    private Animator _animator;
    
    private EffectController _effectController;
    private HPBar _hpBar;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;


    private void Awake()
    {
        _effectController = GetComponent<EffectController>();
        _stat = GetComponent<Stat>();
        _hpBar = gameObject.FindChild<HPBar>(null, true) ?? Managers.UI.MakeWorldSpaceUI<HPBar>(transform, "UI_HPBar");
        _hpBar.transform.localPosition = new Vector3(0f, 2f, 0f);
        _animator = GetComponentInChildren<Animator>(true);
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _collider.bounds
        
        InitStrategy();
    }

    private void Update()
    {
        _btMachine?.Operate();
    }

    public event Action<BaseCharacter> DeathActionEvent;

    // 공격, 이동, 기술 전략 컴포넌트를 초기화
    private void InitStrategy()
    {
        _abstractAttack = GetComponent<AbstractAttack>();
        _abstractAttack?.Init(this, _stat);
        _abstractSkill = GetComponent<AbstractSkill>();
        _abstractSkill?.Init(this, _stat);
        _abstractMove = GetComponent<AbstractMove>();
        _abstractMove?.Init(this, _stat);
    }

    // 행동 트리를 설정
    public void SetBehaviourTree(BTMachine btMachine)
    {
        _btMachine = btMachine;
    }

    // 공격 애니메이션을 재생하고 공격을 실행
    public void Attack()
    {
        PlayAnimation("attack");
        _abstractAttack?.Attack();
    }

    // 스킬 사용 애니메이션을 재생하고 스킬을 사용
    public void UseSkill()
    {
        PlayAnimation("attack");
        _abstractSkill?.UseSkill();
    }

    // 이동 대상이 있는지 확인 후 이동
    public bool HasMoveTarget()
    {
        return _abstractMove.Move();
    }

    // 이동 애니메이션을 재생하며 이동 실행
    public void Move()
    {
        if (!IsAnimationPlaying("walk")) PlayAnimation("walk");
    }

    // 승리 애니메이션을 재생
    public void Victory()
    {
        PlayAnimation("victory");
    }

    // 피해를 입고 상태를 업데이트
    public void Hurt(float damage)
    {
        _stat.Hp -= damage;
        HurtEvent?.Invoke();
        if (_stat.Hp <= 0) Dead();
    }

    // 대기 상태 애니메이션을 재생
    public void Idle()
    {
        PlayAnimation("idle");
    }

    // 사망 애니메이션을 재생하고 관련 처리를 실행
    public void Dead()
    {
        PlayAnimation("die");
        _hpBar.DisableHpBar();
        _abstractMove.ClearTarget();
        _effectController.Clear();
    }

    // 사망 애니메이션 종료 후 처리
    public void AnimationDeadEnd()
    {
        DeathActionEvent?.Invoke(this);
        gameObject.SetActive(false);
    }

    // 캐릭터를 부활시키고 초기 상태로 복원
    public void Revive()
    {
        _stat.Hp = _stat.MaxHp;
        gameObject.SetActive(true);
    }

    // 버프 및 디버프 처리
    public void SetBuff(IEffect effect)
    {
        if (effect == null) return;
        _effectController.Add(effect);
    }
    
    // 지정된 이름의 애니메이션을 재생
    private void PlayAnimation(string name)
    {
        _animator.Play(name, -1, 0f);
    }

    // 캐릭터의 사망 여부를 반환
    public bool IsDead()
    {
        return _stat.Hp <= 0;
    }

    // 특정 애니메이션의 재생 여부를 확인
    public bool IsAnimationPlaying(string animName)
    {
        var animInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.IsName(animName) && animInfo.normalizedTime >= 0 && animInfo.normalizedTime < 1f) return true;
        return false;
    }

    // 공격 쿨다운 상태인지 확인   
    public bool IsAttackCooldown()
    {
        return _abstractAttack ? _abstractAttack.bCoolTime : false;
    }

    // 스킬 쿨다운 상태인지 확인
    public bool IsSkillCooldown()
    {
        return _abstractSkill ? _abstractSkill.bCoolTime : false;
    }

    // 공격 범위 안에 적이 있는지 확인
    public bool IsAttackRange()
    {
        return _abstractAttack ? _abstractAttack.IsInRange() : false;
    }

    // 스킬 범위 안에 적이 있는지 확인
    public bool IsSkillRange()
    {
        return _abstractSkill ? _abstractSkill.IsInRange() : false;
    }

    // 걷기 또는 대기 애니메이션만 재생 중인지 확인
    public bool IsOnlyWalkOrIdleAnimationPlaying()
    {
        var animInfo = _animator.GetCurrentAnimatorStateInfo(0);
        return animInfo.IsName("walk") || animInfo.IsName("idle") || animInfo.normalizedTime >= 1f;
    }
}