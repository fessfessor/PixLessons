using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerLogic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0F;

    [SerializeField]
    private Transform target;

    [SerializeField] public bool bossCamera = false;

    [SerializeField] GameObject mainBackground;

    private Transform backPosition;

    private void Awake() {
        if (!target) target = FindObjectOfType<Player>().transform;
    }

    private void Start() {
        backPosition = mainBackground.transform;
    }

    private void Update() {
        Vector3 position = target.position;
        if (!bossCamera)
        {
            position.x += 5f;
            position.z = -10.0F;
            position.y += 1.0F;
            //интерполяция положения камеры для плавности
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        }
        else
        {
            position.x += 0f;
            position.z = -10.0F;
            position.y += 1.0F;
            //интерполяция положения камеры для плавности
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
        }
        
    }
}
