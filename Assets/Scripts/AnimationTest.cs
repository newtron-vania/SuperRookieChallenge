using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    [SerializeField] private GameObject go;
    private Animator _animator;
    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        Managers.Resource.Instantiate("Archer");
    }
}
