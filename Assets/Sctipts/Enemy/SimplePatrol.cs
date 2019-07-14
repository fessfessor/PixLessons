using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rigthBorder;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Health healthComponent;
    [SerializeField] private Collider2D collider;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

    public bool isRightDirection;

    [Range(1.0f, 10.0f)]
    public float speed = 1.0f;


    private int currentHealth;
    private bool isRised;
   


    private void Start() {
        healthComponent = GetComponent<Health>();
        isRised = false;

    }

    private void Update() {
        

        currentHealth = healthComponent.HealthCount;
        //Debug.Log(currentHealth);
        if (currentHealth <= 0)
            Death();

        if (isRised && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
            collider.enabled = true;
            rb.gravityScale = 1;
            Move();
        } else {
            NonCollision();
        }
            


    }

    

    public void Rise() {
        animator.SetTrigger("Rise");       
        isRised = true;
    }


    void Death() {
        animator.SetTrigger("isDeath");
        NonCollision();
    }

    // отключаем коллизию , на сулчай смерти или же пока скелет еще закопан в земле, об него нельзя было пораниться
    void NonCollision() {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        collider.enabled = false;
    }


    //Простое движение
    void Move() {
        if (isRightDirection && currentHealth > 0) {
            sr.flipX = true;
            rb.velocity = Vector2.right * speed;
            if (transform.position.x > rigthBorder.transform.position.x) {
                isRightDirection = false;
            }
        }
        else if (!isRightDirection && currentHealth > 0) {
            rb.velocity = Vector2.left * speed;
            sr.flipX = false;
            if (transform.position.x < leftBorder.transform.position.x) {
                isRightDirection = true;
            }
        }
        else if (currentHealth < 0)
            rb.velocity = Vector3.zero; // Тормозим объект если убили
    }


}
