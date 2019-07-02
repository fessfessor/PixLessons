using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isGrounded;

    private int countCollision;

    private void OnCollisionEnter2D(Collision2D col) {
        // Debug.Log("tag - " + col.gameObject.transform.parent.transform.tag);
        Transform parent = col.gameObject.transform.parent;
        //Debug.Log("Enter - " + countCollision);
        if (parent) {
            if (parent.name == "Ground" || parent.CompareTag("Ground")) {
                countCollision++;
                isGrounded = countCollision > 0 ? true : false;
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D col) {
        Transform parent = col.gameObject.transform.parent;
        //Debug.Log("Exit - " + countCollision);
        if (parent) {
            if (parent.name == "Ground" || parent.CompareTag("Ground")) {
                countCollision--;
                isGrounded = countCollision > 0 ? true : false;
            }
        }
    }
}
