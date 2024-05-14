using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : UI_Base
{
    private BaseCharacter _character;
    private Stat _stat;
    [SerializeField] private float _visibleTime = 1f;

    private Slider _slider;
    enum GameObjects
    {
        HPBar,
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Start()
    {
        _character = transform.parent.GetComponent<BaseCharacter>();
        _stat = transform.parent.GetComponent<Stat>();
        _slider = GetObject((int)GameObjects.HPBar).GetComponent<Slider>();
        _character.HurtEvent -= UpdateHpBar;
        _character.HurtEvent += UpdateHpBar;
    }

    private Coroutine _coroutine;
    public void UpdateHpBar()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(OnGameObjectVisible());
        float ratio = _stat.Hp / _stat.MaxHp;
        setHpRatio(ratio);
    }

    public void DisableHpBar()
    {
        this.gameObject.SetActive(false);
    }

    public void setHpRatio(float ratio)
    {
        if (ratio < 0)
            ratio = 0;
        if (ratio > 1)
            ratio = 1;
        _slider.value = ratio;
    }

    IEnumerator OnGameObjectVisible()
    {
        this.gameObject.SetActive(true);
        yield return new WaitForSeconds(_visibleTime);
        DisableHpBar();
    }
}
