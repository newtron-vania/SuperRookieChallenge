using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicCameraTargetSetter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private CameraController _camera;
    [SerializeField] private int index = 0;
    private BasicGameController _controller;
    
    void Start()
    {
        _controller = Managers.Game.GetGameController() as BasicGameController;
        _camera = Camera.main.GetComponent<CameraController>();
        SetTarget();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetButton("eventQ") || Input.GetButton("eventE"))
        {
            if (Input.GetButton("eventQ"))
            {
                int checkCount = 0;
                do
                {
                    index++;
                    if (index >= _controller._characters.Count)
                    {
                        index = 0;
                    }
                    
                    checkCount++;
                    if (checkCount >= _controller._characters.Count)
                    {
                        return;
                    }
                } while (!_controller._characters[index].IsDead());
                
            }

            if (Input.GetButton("eventE"))
            {
                int checkCount = 0;
                do
                {
                    index--;
                    if (index < 0)
                    {
                        index = _controller._characters.Count - 1;
                    }
                    
                    checkCount++;
                    if (checkCount >= _controller._characters.Count)
                    {
                        return;
                    }
                } while (!_controller._characters[index].IsDead());
            }
            SetTarget();
        }
    }

    public void SetTarget()
    {
        if (index >= _controller._characters.Count && _controller._characters[index])
        {
            _target = _controller._characters[index].transform;
        }
        
        _camera.SetTarget(_target);
    }
}
