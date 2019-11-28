using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpikes : MonoBehaviour
{
    private Collider2D col;
   
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    

    public void CollisionStart() {
        col.enabled = true;
    }

    public void CollisionEnd() {
        col.enabled = false;
    }
}
