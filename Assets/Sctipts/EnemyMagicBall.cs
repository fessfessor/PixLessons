using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicBall : MonoBehaviour
{
    [SerializeField] private Animator animator;



    public void DestroyBall() {
        animator.SetTrigger("isExplosion");
        Destroy(gameObject, 0.5f);

    }
}
