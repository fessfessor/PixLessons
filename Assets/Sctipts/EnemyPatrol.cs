using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rigthBorder;
    public GameObject head;

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

    Vector2 startHead;

    bool closeAttack = false;

    private int currentHealth;
    private void Start() {
        healthComponent = transform.GetComponent<Health>();
        isWaiting = true;
        isAttacking = false;

        startHead = head.transform.localPosition;

    }

    private void Update() {

        currentHealth = healthComponent.health;
        animator.SetInteger("health", currentHealth);

 
        
        // Некое подобие ИИ, враг патрулирует территории, иногда останавливается отдохнуть, 
        // и затем снова на идет патрулировать. Ускоряется если видит игрока
        if (isEnableAI) {
            // проверяем область патрулирования
            RaycastHit2D hits = CheckArea();
            isAttacking = hits.transform.name == "Player";
            //Debug.Log(hits.transform.name);

            if (!isAttacking) {// Если не атакуем ,то просто гуляем
                if (!isWaiting) //Если мы не ждем, то стартуем корутину с  временем ожидания                
                    Move();
                else
                    StartCoroutine(runAndWait());


                animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));
                animator.speed = 1f;
            }
            else {
                MoveToEnemy(hits.transform.position);
            } 


        }else {
            Move();
        }

    }

    
    IEnumerator runAndWait() {
        yield return new WaitForSeconds(2f);
        isWaiting = false;
        yield return new WaitForSeconds(5f);
        if(!isAttacking)
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
        Debug.DrawLine(transform.position, head.transform.position, Color.red);

        //В зависимости от положения спрайта меняем точку к которой рисуем луч
        if (!sr.flipX)
            head.transform.localPosition = startHead;
        else
            head.transform.localPosition = new Vector2(-startHead.x, startHead.y);

        return Physics2D.Linecast( head.transform.position, transform.position);

    }

   

    void MoveToEnemy(Vector2 enemyPosition) {
        //Бежим к врагу с удвоенной скоростью
        
        if (!closeAttack) {
            transform.position = Vector2.MoveTowards(transform.position, enemyPosition, Time.deltaTime * speed * 3.5f);
            animator.SetFloat("velocity", 1f);
            animator.speed = 2f;
        }
        else {
            animator.speed = 1f;
            animator.SetBool("attack", true);
        }

        //Проверка, если подбежали близко , то останавливаемся и кусаем
        if(Mathf.Abs(transform.position.x - enemyPosition.x) < 1.3f)           
            closeAttack = true;
        else 
            closeAttack = false;
        
    }
}
