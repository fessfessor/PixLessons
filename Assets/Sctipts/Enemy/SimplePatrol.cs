using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrol : MonoBehaviour, IEnemy
{
    #region variables
    [SerializeField] private GameObject leftBorder;
    [SerializeField] private GameObject rightBorder;
 
    [SerializeField] private ENEMY_DANGER dangerClass;
    [SerializeField] public int damage;


   

    [Range(1.0f, 10.0f)]
    public float speed = 1.0f;


    private int currentHealth;
    [SerializeField]private bool isRised;    
    private bool isDamaged;
    [HideInInspector] public bool isDeath = false;
    private Vector3 startPosition;
    private Vector3 leftBorderPosition;
    private Vector3 rightBorderPosition;
    private bool isAttacking = false;
    private EnemyBase enemyBase;
    private AttackArea attackArea;
    private GameObject enemy;

    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D coll;
    [HideInInspector] public Animator animator;
    [HideInInspector] public SpriteRenderer sr;







    public bool isRightDirection;
    #endregion


    #region startUpdate
    private void Start() {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        enemyBase = GameManager.Instance.enemyBase;

        currentHealth = GetComponent<Health>().HealthCount;

        attackArea = GetComponent<AttackArea>();

        EventManager.Instance.AddListener(EVENT_TYPE.HEALTH_CHANGE, OnEvent);
        GameManager.Instance.enemyDangerContainer.Add(gameObject, dangerClass);
        


        
        startPosition = transform.position;
        leftBorderPosition = leftBorder.transform.position;
        rightBorderPosition = rightBorder.transform.position;
        isDamaged = false;
        //isRised = false;

        


    }


    private void Update() {
        
        //Проверяем изменение здоровья
        if (currentHealth > GameManager.Instance.healthContainer[gameObject].HealthCount) {
            isDamaged = true;
        }else 
            isDamaged = false;
            
        
        currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
        
        

        //Debug.Log(currentHealth);
        if (currentHealth <= 0 && !isDeath)
            Death();

        if (isRised && !animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) {
            coll.enabled = true;
            rb.gravityScale = 1;
            Move();
        } else {
            NonCollision();
        }

    }

    #endregion


    public virtual void Rise() {
        if (!isRised) {
            animator.SetTrigger("Rise");
            AudioManager.Instance.Play("Zomby");
            AudioManager.Instance.Play("ZombyRaise");
            isRised = true;
        }
               
    }


    public virtual void Death() {
        isDeath = true;
        AudioManager.Instance.Play("ZombyDie");
        AudioManager.Instance.Stop("Zomby");
        
        animator.SetTrigger("Death");      
        NonCollision();
    }

    // отключаем коллизию , на сулчай смерти или же пока скелет еще закопан в земле, об него нельзя было пораниться
    void NonCollision() {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        coll.enabled = false;
    }


    //Простое движение
    public void Move() {
        if (!isAttacking) {
            if (isRightDirection && currentHealth > 0) {
                //sr.flipX = true;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                rb.velocity = Vector2.right * enemyBase.GetEnemyOfID(0).Speed;
                if (transform.position.x > rightBorderPosition.x) {
                    isRightDirection = false;
                }
            }
            else if (!isRightDirection && currentHealth > 0) {
                rb.velocity = Vector2.left * enemyBase.GetEnemyOfID(0).Speed;
                //sr.flipX = false;
                transform.rotation = Quaternion.Euler(0, 0, 0);
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
        //Debug.Log($"Изменилось здоровье у {sender.name}. Стало {health.HealthCount}");       
    }


    

    //Вызывается из дочернего объекта "Attack Area"
    public void Attack(bool isAttacking, GameObject enemy) {
        this.isAttacking = isAttacking;
        this.enemy = enemy;

        if (isAttacking) {
            animator.SetTrigger("Attack");
        }
        else { 
            animator.SetTrigger("EndAttack");
        }
    }

    public void Damage() {
        if (isAttacking && enemy!=null) {
            GameManager.Instance.healthContainer[enemy].takeHit(damage);
        }
    }



}
