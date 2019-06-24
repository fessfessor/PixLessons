using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public GameObject leftBorder;
    public GameObject rigthBorder;
    

    public Rigidbody2D rb;
    public Health healthComponent;
    [SerializeField] Collider2D collider;
   
    public Animator animator;

    public SpriteRenderer sr;

    public bool isRightDirection;

    [Range(1.0f,10.0f)]
    public float speed = 1.0f;

    // Параметры для врагов с измененным поведением
    public bool isEnableAI;
    

    public bool isWaiting;
    public bool isAttacking;
    public bool checkArea;

    bool closeAttack;

    private int currentHealth;
    private RaycastHit2D[] hits;


    private void Start() {
        healthComponent = transform.GetComponent<Health>();
        isWaiting = true;
        isAttacking = false;
        closeAttack = false;
        checkArea = false;

        if(isEnableAI)
            StartCoroutine(checkAreaCour());


    }

    private void Update() {

        currentHealth = healthComponent.HealthCount;
        
        animator.SetInteger("health", currentHealth);

        //отключаем коллайдер, чтобы анимация смерти не дамажила
        if (currentHealth < 0) {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            collider.enabled = false;
            
        }
            
            
        
        // Некое подобие ИИ, враг патрулирует территории, иногда останавливается отдохнуть, 
        // и затем снова на идет патрулировать. Ускоряется если видит игрока
        if (isEnableAI) {
            if(closeAttack)
                animator.SetBool("attack", false);


            // Если в большой луч попал игрок бежим к нему
            if(hits[0] && !hits[1]) {
                isAttacking = hits[0].transform.name == "Player";
                closeAttack = false;

                MoveToEnemy(hits[0].transform.position);
                
                //Debug.Log(hits[0].transform.name);
            }
            else if(hits[1]) {
                Debug.Log("CLOSE");
                isAttacking = hits[1].transform.name == "Player";
                closeAttack = hits[1].transform.name == "Player";
                MoveToEnemy(hits[0].transform.position);
            }
            else {
                closeAttack = false;
                isAttacking = false;
            }
                

            if (!isAttacking) {// Если не атакуем ,то просто гуляем

                if (!isWaiting) //Если мы не ждем, то стартуем корутину с  временем ожидания                
                    Move();
                else
                    StartCoroutine(runAndWait());

                animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));
                animator.speed = 1f;
            }
            


        }else {
            Move();
        }

    }


    IEnumerator checkAreaCour() {
        while (true) {
            hits = CheckArea();
            yield return new WaitForSeconds(0.5f);
            
        }           
    }
    
    //Создает видимость прогулки с моментами отдыха
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
    RaycastHit2D[] CheckArea() {
        //Debug.Log("CHECK");
        Ray2D rayLeft = new Ray2D(transform.position, transform.position * Vector2.left);
        Ray2D rayRight = new Ray2D(transform.position, transform.position * Vector2.right);
        
        Debug.DrawRay(transform.position, rayLeft.direction * 10, Color.white);
        Debug.DrawRay(transform.position, rayRight.direction * 10, Color.white);
        Debug.DrawRay(transform.position, rayLeft.direction * 1.4f, Color.red);
        Debug.DrawRay(transform.position, rayRight.direction * 1.4f, Color.red);

        RaycastHit2D[] rays = new RaycastHit2D[2];

        //В зависимости от положения спрайта меняем направление лучей
        if (!sr.flipX) {
            rays[0] = Physics2D.Raycast(transform.position, rayLeft.direction, 10f);
            rays[1] = Physics2D.Raycast(transform.position, rayLeft.direction, 1.4f);
        }
        else {
            rays[0] = Physics2D.Raycast(transform.position, rayRight.direction, 10f);
            rays[1] = Physics2D.Raycast(transform.position, rayRight.direction, 1.4f);
        }
        
        return rays;
    }

    

   

    void MoveToEnemy(Vector2 enemyPosition) {
        //Бежим к врагу с удвоенной скоростью
        if (currentHealth < 0)
            return;

        if (!closeAttack) {
            transform.position = Vector2.MoveTowards(transform.position, enemyPosition, Time.deltaTime * speed * 3.5f);           
            animator.SetFloat("velocity", 1f);
            animator.speed = 2f;
        }
        else {
            //transform.position = Vector2.MoveTowards(transform.position, enemyPosition, Time.deltaTime * speed * 0.5f);
            animator.speed = 1f;
            animator.SetBool("attack", true);

        }

    }
}
