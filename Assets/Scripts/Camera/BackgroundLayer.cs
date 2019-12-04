using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLayer : MonoBehaviour
{

    [SerializeField] int currentLayer;
    [SerializeField] float backgroundHeight;
    [SerializeField] GameObject backgroundPicture;
    

    private Transform cameraTranform;
    private float anchorPoint;
    


    void Start()
    {
        cameraTranform = Camera.main.transform;
        


    }

    
    void Update()
    {
        if (GameManager.Instance.player.transform.position.y > backgroundHeight/2 + backgroundHeight*currentLayer) {
            currentLayer++;
        } else if(GameManager.Instance.player.transform.position.y < - backgroundHeight/2 + backgroundHeight * currentLayer) {
            currentLayer--;
        }

            // Картинка бэкграунда
            backgroundPicture.transform.position = new Vector3(cameraTranform.position.x, 
                                                           backgroundPicture.transform.position.y, 
                                                           backgroundPicture.transform.position.z);
        
        transform.position = new Vector2( transform.position.x,  currentLayer * backgroundHeight);


        
    }
}
