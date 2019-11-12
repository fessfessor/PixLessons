using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampMotion : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D playerRb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameManager.Instance.player.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject == GameManager.Instance.player) {
            rb.velocity = playerRb.velocity/2;
        }
    }
}
