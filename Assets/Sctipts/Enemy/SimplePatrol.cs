using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rigthBorder;
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private Collider2D coll;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;

   

    [Range(1.0f, 10.0f)]
    public float speed = 1.0f;


    private int currentHealth;
    private bool isRised;
    private bool isDamaged;
    


    public bool isRightDirection;




    private void Start() {

        isDamaged = false;
        isRised = false;
        

    }

    private void Update() {
        currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;


       
        

        //Debug.Log(currentHealth);
        if (currentHealth <= 0)
            Death();

        if (isRised && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
            coll.enabled = true;
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
        coll.enabled = false;
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
