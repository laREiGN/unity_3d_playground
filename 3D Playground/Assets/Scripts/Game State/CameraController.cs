using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera isometricLevelCamera;
    [SerializeField] Camera playerCamera;

    void Start()
    {
        isometricLevelCamera.enabled = true;
        playerCamera.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isometricLevelCamera.enabled = !isometricLevelCamera.enabled;
            playerCamera.enabled = !playerCamera.enabled;
        }
    }
}
