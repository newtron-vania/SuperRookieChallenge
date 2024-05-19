using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar,
    }
    
    [SerializeField] private float _visibleTime = 1f;
    private BaseCharacter _character;

    private Coroutine _coroutine;

    private Slider _slider;
    private Stat _stat;

    private void Start()
    {
        _character = transform.parent.GetComponent<BaseCharacter>();
        _stat = transform.parent.GetComponent<Stat>();
        _slider = GetObject((int)GameObjects.HPBar).GetComponent<Slider>();
        _character.HurtEvent -= UpdateHpBar;
        _character.HurtEvent += UpdateHpBar;
        DisableHpBar();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    public void UpdateHpBar()
    {
        EnableHpBar();
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(OnGameObjectVisible());
        var ratio = _stat.Hp / _stat.MaxHp;
        setHpRatio(ratio);
    }

    public void EnableHpBar()
    {
        gameObject.SetActive(true);
    }
    public void DisableHpBar()
    {
        gameObject.SetActive(false);
    }

    public void setHpRatio(float ratio)
    {
        if (ratio < 0)
            ratio = 0;
        if (ratio > 1)
            ratio = 1;
        _slider.value = ratio;
    }

    private IEnumerator OnGameObjectVisible()
    {
        gameObject.SetActive(true);
        yield return new WaitForSeconds(_visibleTime);
        DisableHpBar();
    }

}