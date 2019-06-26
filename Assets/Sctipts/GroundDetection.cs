using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isGrounded;

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.transform.parent.transform.name == "Ground") {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.transform.parent.transform.name == "Ground") {
            isGrounded = false;
        }
    }
}
