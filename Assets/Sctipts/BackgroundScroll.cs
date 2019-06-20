using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Transform cameraTranform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;

    public float backgroundSize;

    private void Start() {
        cameraTranform = Camera.main.transform;
        layers = new Transform[transform.childCount];

        for(int i=0; i<transform.childCount; i++) {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void Update() {
       if(cameraTranform.position.x < (layers[leftIndex].transform.position.x + viewZone)) {
            ScrollLeft();
        }
        if (cameraTranform.position.x > (layers[rightIndex].transform.position.x - viewZone)) {
            ScrollRight();
        }
    }

    void ScrollLeft() {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
            rightIndex = layers.Length - 1;
    }

    void ScrollRight() {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[leftIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
            leftIndex = 0;
    }
}
