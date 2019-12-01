using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{

    public GameObject attackArea;



    private Collider2D attackAreaCollider;
    void Start()
    {
        attackAreaCollider = attackArea.GetComponent<Collider2D>();
    }

    
    void Update()
    {
        
    }
    
    
}
