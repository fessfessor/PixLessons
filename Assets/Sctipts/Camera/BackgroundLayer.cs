using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{

    [SerializeField] int currentLayer;
    [SerializeField] float backgroundHeight;

    

    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = new Vector2( transform.position.x,  currentLayer * backgroundHeight);
    }
}
