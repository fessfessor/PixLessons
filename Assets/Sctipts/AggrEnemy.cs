using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggrEnemy : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rigthBorder;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckArea().transform.name == "Player") {

        }
            
          
            
    }

    
    //Проверяем территории между 2 границ
    RaycastHit2D CheckArea() {    
        return Physics2D.Linecast(leftBorder.transform.position, rigthBorder.transform.position);
    }

    void Move() {

    }
}
