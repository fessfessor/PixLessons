using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 cameraInitialPosition;
    [SerializeField] private float shakeMagnitude = 0.05f, shakeTime = 0.05f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject bloodOnScreen;
    private SpriteRenderer bloodRender;

    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float fadeTimeBlood = 1f;
    [SerializeField] private float fadeTimeBloodOut = 2f;
    [SerializeField] private float fadedelay = 3f;

    private void Start() {        
        bloodRender = bloodOnScreen.GetComponent<SpriteRenderer>();
    }

    public void Shake() {
        //Debug.Log("Shake");
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f,0.01f);
        Invoke("StopCameraShaking", shakeTime);
        BloodySpray();
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

    void BloodySpray() {
        
        
        StartCoroutine(LerpAlpha());
    }

    IEnumerator LerpAlpha() {
        
        for (float t = 0.01f;t < fadeTimeBlood; t += 0.1f) {        
            bloodRender.color = Color.Lerp(startColor, endColor, t / fadeTimeBlood);
            yield return null;
        }
        
        StartCoroutine(LerpAlphaOut());
    }

    IEnumerator LerpAlphaOut() {       
        for (float t = 0.01f; t < fadeTimeBloodOut; t += 0.1f) {
            bloodRender.color = Color.Lerp(endColor, startColor, t / fadeTimeBloodOut);
            yield return null;
        }
    }
}


