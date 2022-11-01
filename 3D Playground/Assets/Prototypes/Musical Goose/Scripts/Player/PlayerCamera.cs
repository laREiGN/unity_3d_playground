using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 _offset;
    private Vector3 _currentVelocity = Vector3.zero;

    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float smoothingEffect;

    private void Awake()
    {
        _offset = transform.position - cameraTarget.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = cameraTarget.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothingEffect);
    }
}
