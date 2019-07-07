using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicBall : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Collider2D collider;



    public void DestroyBall() {
        animator.SetTrigger("isExplosion");
        collider.enabled = false;
        Destroy(gameObject, 0.5f);

    }
}
