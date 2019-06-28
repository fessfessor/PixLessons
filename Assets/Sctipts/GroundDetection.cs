using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isGrounded;

    private void OnCollisionEnter2D(Collision2D col) {
        // Debug.Log("tag - " + col.gameObject.transform.parent.transform.tag);
        Transform parent = col.gameObject.transform.parent;
        if (parent) {
            if (parent.name == "Ground" || parent.CompareTag("Ground")) {
                isGrounded = true;
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D col) {
        Transform parent = col.gameObject.transform.parent;
        if (parent) {
            if (parent.name == "Ground" || parent.CompareTag("Ground")) {
                isGrounded = false;
            }
        }
    }
}
