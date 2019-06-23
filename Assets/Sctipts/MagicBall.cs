using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    public float Force { get => force; set => force = value;}

    public void SetImpulse(Vector2 direction, float force) {
        rb.AddForce(direction* force, ForceMode2D.Impulse);
    }


    
}
