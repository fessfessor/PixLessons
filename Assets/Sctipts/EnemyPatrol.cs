using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rigthBorder;
    public Rigidbody2D rb;
    public Health healthComponent;
   
    public Animator animator;

    public SpriteRenderer sr;

    public bool isRightDirection;

    [Range(1.0f,10.0f)]
    public float speed = 1.0f;

    // Параметры для врагов с измененным поведением
    public bool isEnableAI;
    public string animationIdle;
    public string animationWalk;
    public string animationBite;

    public bool isWaiting;
    public bool isAttacking;


    private int currentHealth;
    private void Start() {
        healthComponent = transform.GetComponent<Health>();
        isWaiting = true;
        isAttacking = false;
    }

    private void Update() {

        currentHealth = healthComponent.health;
        animator.SetInteger("health", currentHealth);

        //Debug.Log(currentHealth);

        
        // Некое подобие ИИ, враг патрулирует территории, но иногда останавливается отдохнуть, 
        // и затем снова на идет патрулировать
        if (isEnableAI) {
            RaycastHit2D hits = CheckArea();

            if (!isAttacking) {// Если не атакуем ,то просто гуляем
                if (!isWaiting) //Если мы не ждем, то стартуем корутину с  временем ожидания                
                    Move();
                else
                    StartCoroutine(runAndWait());
            }  
            
            // в территорию патрулирования зашел враг               
            if (hits.transform.name == "Player") {
                isAttacking = true;
                MoveToEnemy(hits.transform.position);// бежим к нему и кусаем пока кто-нибдуь не умрет или не попросит пощады
            }
            
            

            animator.SetFloat("velocity", Mathf.Abs( rb.velocity.x));
            // проверяем область патрулирования

           

        }else {
            Move();
        }

    }

    
    IEnumerator runAndWait() {
        yield return new WaitForSeconds(2f);
        isWaiting = false;
        yield return new WaitForSeconds(5f);
        isWaiting = true;
    }


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
            rb.velocity = new Vector2(0, 0); // Тормозим объект если убили
    }

    //Проверяем территории между 2 границ
    RaycastHit2D CheckArea() {
        return Physics2D.Linecast(leftBorder.transform.position, rigthBorder.transform.position);
    }

    void MoveToEnemy(Vector2 enemyPosition) {


    }
}
