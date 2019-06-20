using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public float force = 1.0f;
    public Rigidbody2D rb;   
    public float minHeight = -50.0f;
    public GroundDetection groundD;
    private Vector3 direction;
    public SpriteRenderer spriteR;

    public Animator animator;

    public GameObject SwordRight;
    public GameObject SwordLeft;
    public bool isRightDirection;

    private Vector3 jumpDirection;
    private bool isJumping;
    private bool canMove;
    private bool canAttack;
    public bool isAttacking;

    public float swordAttackTime;
    

    public float timeRemaining = 3.0f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        canAttack = true;
        canMove = true;
              
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Анимации
        animator.SetBool("isGrounded", groundD.isGrounded);
        if(!isJumping && !groundD.isGrounded)
            animator.SetTrigger("fallWithoutJump");

        isJumping = !groundD.isGrounded;

        animator.SetFloat("speed", Mathf.Abs(direction.x));
        animator.SetFloat("isFalling", rb.velocity.y);


        //Движение
        if (canMove) {
            Move();
        }
        


        // Обработка прыжка
        if ( Input.GetKeyDown(KeyCode.Space) && groundD.isGrounded) {
            Jump();
            animator.SetTrigger("startJump");
        }

        //Атака
        if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack){
            StartCoroutine(SwordAttack());
           // canMove = false;
           // canAttack = false;
        }

        // телепортация обратно на платформу
        if (transform.position.y < minHeight)
            resetHeroPoition();

        if (direction.x > 0) {
            isRightDirection = true;
            spriteR.flipX = false;           
        }
        if (direction.x < 0) {
            isRightDirection = false;
            spriteR.flipX = true;
        }

        

        //Потрясающий костыль, решающий проблему ,что когда коллайдер оружия появляется во враге и их кколлайдеры не движутся
        //относительно друг друга коллизии не происходит. Эта строка при каждой атаке совсем чуть двигает персонажа и вызвает коллизию
        if (isAttacking)
            transform.position = Vector3.Lerp(transform.position, new Vector2(transform.position.x + 0.0001f, transform.position.y), 1f);


    }
   
   // Корутина чтобы останавливать персонажа,когда он бьет мечом 
   // и чтобы нельзя было закликивать атаку
   IEnumerator SwordAttack() {
        Attack();
        isAttacking = true;
        yield return new WaitForSeconds(swordAttackTime);
        isAttacking = false;
        canMove = true;
        canAttack = true;
    }
    

    
    void Jump() {
        isJumping = true;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    void Attack() {
       canAttack = false;
       canMove = false;       
        animator.SetTrigger("isSwordAttack");       


    }
    
    //методы для ивента. Появление\исчезание коллайдера меча
    void SwordAttackColliderStart() {
        if (isRightDirection)
            SwordRight.SetActive(true);        
        else
            SwordLeft.SetActive(true);
    }

    void SwordAttackColliderDone() {
        if (isRightDirection)
            SwordRight.SetActive(false);
        else
            SwordLeft.SetActive(false);
    }

    void Move() {
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            direction = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D)) {
            direction = Vector3.right;
        }
        direction *= speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
    }

    void resetHeroPoition() {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
    }

   


}
