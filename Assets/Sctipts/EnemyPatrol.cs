using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rigthBorder;
    public Rigidbody2D rb;

    public bool isRightDirection;

    [Range(1.0f,10.0f)]
    public float speed = 1.0f;


    private void Update() {
        if (isRightDirection) {
            rb.velocity = Vector2.right * speed;
            if(transform.position.x > rigthBorder.transform.position.x) {
                isRightDirection = false;
            }
        }
        else {
            rb.velocity = Vector2.left * speed;
            if (transform.position.x < leftBorder.transform.position.x) {
                isRightDirection = true;
            }
        }
    }
}
