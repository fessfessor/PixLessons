using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rigthBorder;
    public Rigidbody2D rb;
    public Health healthComponent;
    private int currentHealth;
    public Animator animator;


    public SpriteRenderer sr;

    public bool isRightDirection;

    [Range(1.0f,10.0f)]
    public float speed = 1.0f;

    private void Start() {
        healthComponent = transform.GetComponent<Health>();
    }

    private void Update() {

        currentHealth = healthComponent.health;
        animator.SetInteger("health", currentHealth);

        Debug.Log(currentHealth);


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
            rb.velocity = new Vector2(0, 0); // Тормозим объект если убили
        

    }
}
