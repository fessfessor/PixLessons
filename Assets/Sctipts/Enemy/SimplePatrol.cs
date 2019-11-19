using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    #region variables
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
    [SerializeField] private Rigidbody2D rb;
    
    [SerializeField] private Collider2D coll;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private ENEMY_DANGER dangerClass;

   

    [Range(1.0f, 10.0f)]
    public float speed = 1.0f;


    private int currentHealth;
    private bool isRised;
    private bool isDamaged;
    private bool isDeath = false;
    private Vector3 startPosition;
    private Vector3 leftBorderPosition;
    private Vector3 rightBorderPosition;
    private bool isAttacking = false;




    public bool isRightDirection;
    #endregion


    #region startUpdate
    private void Start() {
        EventManager.Instance.AddListener(EVENT_TYPE.HEALTH_CHANGE, OnEvent);
        GameManager.Instance.enemyDangerContainer.Add(gameObject, dangerClass);
        currentHealth = GetComponent<Health>().HealthCount;

        startPosition = transform.position;
        leftBorderPosition = leftBorder.transform.position;
        rightBorderPosition = rightBorder.transform.position;
        isDamaged = false;
        isRised = false;
        

    }


    private void Update() {
        //Проверяем изменение здоровья
        if (currentHealth > GameManager.Instance.healthContainer[gameObject].HealthCount) {
            isDamaged = true;
            //Уведомляем слушателей, что у скелета изменилось здоровье
            EventManager.Instance.PostNotification(EVENT_TYPE.HEALTH_CHANGE, this, GameManager.Instance.healthContainer[gameObject]);
        }
        else 
            isDamaged = false;
            
        
        currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;

        

        //Debug.Log(currentHealth);
        if (currentHealth <= 0 && !isDeath)
            Death();

        if (isRised && animator.GetCurrentAnimatorStateInfo(0).IsName("Walk")) {
            coll.enabled = true;
            rb.gravityScale = 1;
            Move();
        } else {
            NonCollision();
        }

    }

    #endregion


    public void Rise() {
        if (!isRised) {
            animator.SetTrigger("Rise");
            AudioManager.Instance.Play("Zomby");
            AudioManager.Instance.Play("ZombyRaise");
            isRised = true;
        }
               
    }


    void Death() {
        isDeath = true;
        AudioManager.Instance.Play("ZombyDie");
        AudioManager.Instance.Stop("Zomby");
        
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
        if (!isAttacking) {
            if (isRightDirection && currentHealth > 0) {
                sr.flipX = true;
                rb.velocity = Vector2.right * speed;
                if (transform.position.x > rightBorderPosition.x) {
                    isRightDirection = false;
                }
            }
            else if (!isRightDirection && currentHealth > 0) {
                rb.velocity = Vector2.left * speed;
                sr.flipX = false;
                if (transform.position.x < leftBorderPosition.x) {
                    isRightDirection = true;
                }
            }
            else if (currentHealth < 0)
                rb.velocity = Vector3.zero; // Тормозим объект если убили
        }
        
    }

   
    private void OnEvent(EVENT_TYPE eventType, Component sender, object param = null) {
        switch (eventType) {
            case EVENT_TYPE.GAME_INIT:
                break;
            case EVENT_TYPE.GAME_END:
                break;
            case EVENT_TYPE.HEALTH_CHANGE:
                OnHealthChange(sender, (Health)param);
                break;
            case EVENT_TYPE.PLAYER_DEATH:
                break;
            default:
                break;
        }
 
    }

    private void OnHealthChange(Component sender, Health health) {
        Debug.Log($"Изменилось здоровье у {sender.name}. Стало {health.HealthCount}");       
    }



    public void Attack(bool isAttacking) {
        this.isAttacking = isAttacking;

        if (isAttacking)                   
            Debug.Log("Skeleton attack!");
        else
            Debug.Log("Skeleton STOP attack!");
    }
    
}
