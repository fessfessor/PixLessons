using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGroundDetection : MonoBehaviour
{

    public bool isGrounded;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.CompareTag("Ground")) {
            
            if (rb.velocity.y < 0.01f)
                isGrounded = true;

            
        }


    }



    private void OnCollisionExit2D(Collision2D col) {

        if (col.gameObject.CompareTag("Ground")) {
            
            if (rb.velocity.y > 0.01)
                isGrounded = false;
            

        }
    }
}
