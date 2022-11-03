using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Vector3 _highCamOffset;
    private Vector3 _currentVelocity = Vector3.zero;
    private Camera cameraUsed;
    private Vector3 offsetUsed;

    [SerializeField] private Camera highCamera;
    [SerializeField] private Camera lowCamera;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float smoothingEffect;

    private bool highCamUsed;

    private void Awake()
    {
        _highCamOffset = highCamera.transform.position - cameraTarget.position;

        cameraUsed = highCamera;
        offsetUsed = _highCamOffset;
        highCamUsed = true;

        lowCamera.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = cameraTarget.position + offsetUsed;
        if (highCamUsed){
            cameraUsed.transform.position = Vector3.SmoothDamp(cameraUsed.transform.position, targetPosition, ref _currentVelocity, smoothingEffect);
        }
    }

    public void switchCamera(){
        if (highCamUsed){
            lowCamera.gameObject.SetActive(true);
            cameraUsed = lowCamera;
            highCamera.gameObject.SetActive(false);
            highCamUsed = false;
        } else {
            highCamera.gameObject.SetActive(true);
            cameraUsed = highCamera;
            lowCamera.gameObject.SetActive(false);
            highCamUsed = true;
        }
    }
}
