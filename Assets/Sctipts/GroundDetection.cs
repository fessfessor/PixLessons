using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isGrounded;

    private void OnCollisionEnter2D(Collision2D col) {
       // Debug.Log("tag - " + col.gameObject.transform.parent.transform.tag);
        
        if (col.gameObject.transform.parent.transform.name == "Ground" || col.gameObject.transform.parent.transform.CompareTag("Ground")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.transform.parent.transform.name == "Ground" || col.gameObject.transform.parent.transform.CompareTag("Ground")) {
            isGrounded = false;
        }
    }
}
