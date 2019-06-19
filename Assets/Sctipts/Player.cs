using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public float force = 1.0f;
    public Rigidbody2D rb;   
    public float minHeight = -50.0f;
    public SpriteRenderer[] renderers;
    public GroundDetection groundD;
    private Vector3 direction;
    public SpriteRenderer spriteR;

    public Animator animator;

    private Vector3 jumpDirection;
    private bool isJumping;
    private bool canMove;

    public float swordAttackTime;
    

    public float timeRemaining = 3.0f;

    GameObject plat;
    
    // Start is called before the first frame update
    void Start()
    {

        canMove = true;
        plat = GameObject.Find("Platforms");
        renderers = plat.GetComponentsInChildren<SpriteRenderer>();      

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
        if (Input.GetKeyDown(KeyCode.Mouse0)){
            StartCoroutine(SwordAttack());
            canMove = false;
        }

        // телепортация обратно на платформу
        if (transform.position.y < minHeight)
            resetHeroPoition();
        
        if (direction.x > 0)
            spriteR.flipX = false;
        if (direction.x < 0)
            spriteR.flipX = true;

       
    }
   
   // Корутина чтобы останавливать персонажа,когда он бьет мечом 
   IEnumerator SwordAttack() {
        Attack();
        yield return new WaitForSeconds(swordAttackTime);
        canMove = true;
    }
    

    
    void Jump() {
        isJumping = true;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    void Attack() {
        canMove = false;       
        animator.SetTrigger("isSwordAttack");
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
