using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 cameraInitialPosition;
    [SerializeField] private float shakeMagnitude = 0.05f, shakeTime = 0.05f;
    [SerializeField] Camera mainCamera;


    public void Shake() {
        Debug.Log("Shake");
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f,0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking() {
        //Рандомные девиации
        float camX = Random.value * shakeMagnitude * 2 - shakeMagnitude;
        float camY = Random.value * shakeMagnitude * 2 - shakeMagnitude;

        Vector3 camSamplePosition = mainCamera.transform.position;
        camSamplePosition.x += camX;
        camSamplePosition.y += camY;
        mainCamera.transform.position = camSamplePosition;
    }

    void StopCameraShaking() {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }
}


