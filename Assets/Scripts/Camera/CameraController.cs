using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Camera _camera;
    private Transform _target;
    private bool _isCameraActive = true;

    private void Awake()
    {
        _camera = Camera.main;
        if (ReferenceEquals(_camera, null))
        {
            Debug.LogError("Main camera not found!");
            enabled = false;
        }
    }

    private void Update()
    {
        if (ReferenceEquals(_camera, null) || ReferenceEquals(_target, null) || !_isCameraActive) return;

        FollowTarget();
    }

    // 타겟을 설정하는 함수
    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    // 타겟을 따라 부드럽게 이동하는 함수
    private void FollowTarget()
    {
        var desiredPosition = _target.position + offset;
        var smoothedPosition = Vector3.Lerp(_camera.transform.position, desiredPosition, smoothSpeed);
        _camera.transform.position = smoothedPosition;
        _camera.transform.LookAt(_target);
    }

    // 카메라 컨트롤을 중지하는 조건에 대한 함수
    public void SetCameraActive(bool isActive)
    {
        _isCameraActive = isActive;
    }
}