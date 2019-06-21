using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public bool scrolling, parallax;
    public float backgroundSize;
    public float parallaxSpeed;

    private Transform cameraTranform;
    private Transform[] layers; // транформы фонов
    private float viewZone = 10; // расстояние при котором будет скроллиться фон
    private int leftIndex; 
    private int rightIndex;
    private float lastCameraX;

    private void Start() {
        cameraTranform = Camera.main.transform;
        lastCameraX = cameraTranform.position.x;
        layers = new Transform[transform.childCount];

        //заполняем массив фонами
        for(int i=0; i<transform.childCount; i++) {
            layers[i] = transform.GetChild(i);
        }
        //Debug.Log(layers.Length + " layers");

        //инициализируем начальные индексы
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void Update() {
        if (parallax) {
            //Параллакс
            float deltaX = cameraTranform.position.x - lastCameraX;           
            transform.position += new Vector3((deltaX * parallaxSpeed), 0, 0);
             
        }
        lastCameraX = cameraTranform.position.x;


        if (scrolling) {
            if (cameraTranform.position.x < (layers[leftIndex].transform.position.x + viewZone)) {
                ScrollLeft();
            }
            if (cameraTranform.position.x > (layers[rightIndex].transform.position.x - viewZone)) {
                ScrollRight();
            }
        }
    }

    void ScrollLeft() {
        int lastRight = rightIndex;
        // 9 по z потому что родительский объект в -9 по z
        if(layers[rightIndex].CompareTag("BackMount"))
            layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize,0,9);     
        else
            layers[rightIndex].position = new Vector3(layers[leftIndex].position.x - backgroundSize, -3, 8);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    void ScrollRight() {
        int lastLeft = leftIndex;
        //layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        if (layers[rightIndex].CompareTag("BackMount"))
            layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backgroundSize, 0, 9);
        else
            layers[leftIndex].position = new Vector3(layers[rightIndex].position.x + backgroundSize, -3, 8);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
