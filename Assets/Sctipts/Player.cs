using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public float force = 1.0f;
    public Rigidbody2D rb;   
    bool doubleJumpAllowed = false;
    public float minHeight = -50.0f;
    public SpriteRenderer[] renderers;
    public GroundDetection groundD;
    private Vector3 direction;
    public SpriteRenderer spriteR;

    public Animator animator;

    private Vector3 jumpDirection;
    private bool isJumping;

    public float timeRemaining = 3.0f;

    GameObject plat;
    
    // Start is called before the first frame update
    void Start()
    {
        

        plat = GameObject.Find("Platforms");
        renderers = plat.GetComponentsInChildren<SpriteRenderer>();      

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // todo разобраться с падениями
        animator.SetBool("isGrounded", groundD.isGrounded);
        if(!isJumping && !groundD.isGrounded)
            animator.SetTrigger("fallWithoutJump");
        isJumping = !groundD.isGrounded;

        
        direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A)) {
            direction = Vector3.left;
        } else if (Input.GetKey(KeyCode.D)) {
            direction = Vector3.right;
        }
        direction *= speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;

        // Обработка прыжка
        if ( Input.GetKeyDown(KeyCode.Space) && groundD.isGrounded) {
            Jump();
            animator.SetTrigger("startJump");
        }

        // телепортация обратно на платформу
        if(transform.position.y < minHeight) {
            rb.velocity = new Vector2(0,0);
            transform.position = new Vector2(0, 0);
        }

        animator.SetFloat("speed", Mathf.Abs(direction.x));
        animator.SetFloat("isFalling", rb.velocity.y);

        if (direction.x > 0)
            spriteR.flipX = false;
        if (direction.x < 0)
            spriteR.flipX = true;

        
    }

   

    //Метод прыжка
    void Jump() {
        isJumping = true;
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        //Debug.Log(rb.velocity.y);
    }

   


}
