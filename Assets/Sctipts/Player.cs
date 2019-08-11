using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float force = 1.0f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float minHeight = -50.0f;
    [SerializeField] private GroundDetection groundD;   
    [SerializeField] private SpriteRenderer spriteR;   
    [SerializeField] private Animator animator;
    [SerializeField] private CameraShake cameraShaker;
    [SerializeField] private bool ShakeCameraOnDamage;
    [SerializeField] private bool isDamaged;
    public bool IsDamaged { get => isDamaged; }
    [SerializeField] private float swordAttackTime;
    [SerializeField] private float shootForce;
    [SerializeField] private MagicBall magicBall;
    [SerializeField] private int ballsCoulnt = 3;
    [SerializeField] private Health health;
    [SerializeField] private GameObject SwordRight;
    [SerializeField] private GameObject SwordLeft;
    [SerializeField] bool useComputerMode;
    [SerializeField] private Joystick joystick;
    
    
    private  bool isRightDirection;
    private Vector3 jumpDirection;
    private bool isJumping;
    private bool canMove;
    private bool canAttack;
    private Vector3 direction;
    private int currentHealth;
    private bool isAttacking;
    private bool shootReady;
    private ObjectPooler pooler;
    private UIController controller;
    private AudioManager audioManager;

    private Collider2D SwordRightCollider;
    private Collider2D SwordLeftCollider;


    public Health Health { get { return health; } }

    
    // Start is called before the first frame update
    void Start()
    {
        pooler = ObjectPooler.Instance;
        audioManager = AudioManager.Instance;

        isAttacking = false;
        canAttack = true;
        canMove = true;
        shootReady = true;
        isDamaged = false;
        currentHealth = Health.HealthCount;
        cameraShaker = transform.GetComponent<CameraShake>();

        SwordRightCollider = SwordRight.GetComponent<Collider2D>();
        SwordLeftCollider = SwordLeft.GetComponent<Collider2D>();

       // InitUIController();

    }


    void FixedUpdate() {


        //Анимации
        animator.SetBool("isGrounded", groundD.isGrounded);
        if (!isJumping && !groundD.isGrounded)
            animator.SetTrigger("fallWithoutJump");

        isJumping = !groundD.isGrounded;

        animator.SetFloat("speed", Mathf.Abs(direction.x));
        animator.SetFloat("isFalling", rb.velocity.y);


        //Движение
        if (canMove) {
            Move();
        }

        float vetricalMove = joystick.Vertical;
        if (vetricalMove >= .5f) {
            Jump();
        }

#if UNITY_EDITOR
            if (useComputerMode) {
            if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
                Attack();


            if (shootReady && Input.GetMouseButtonDown(1) && groundD.isGrounded && !isAttacking)               
                Shoot();
            

            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
#endif

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

 
    }


    private void Update() {
        

        // Проверка на дамаг
        if (currentHealth > GameManager.Instance.healthContainer[gameObject].HealthCount) {
            isDamaged = true;
            currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
        }
        else {
            isDamaged = false;
            currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
        }

        //При уменьшении здоровья трясем камеру
        if (isDamaged && ShakeCameraOnDamage) 
            cameraShaker.Shake();            

            
    }

    #region Attack

    public void Shoot() {
        if (shootReady && groundD.isGrounded && !isAttacking) 
            StartCoroutine(MagicAttack());          
    }

    IEnumerator MagicAttack() {
        animator.SetTrigger("isShooting");
        isAttacking = true;
        shootReady = false;
        canAttack = false;
        canMove = false;
        rb.velocity = Vector2.zero;
        AudioManager.Instance.Play("FireballCast");
        
        // Время анимации
        yield return new WaitForSeconds(0.8f);
        isAttacking = false;
        canMove = true;
        canAttack = true;
        // Время перезарядки
        yield return new WaitForSeconds(0.8f);
        shootReady = true;      
    }

    

    public void Attack() {
        if (!isAttacking) {
            canAttack = false;
            canMove = false;
            animator.SetTrigger("isSwordAttack");
            StartCoroutine(SwordAttack());
        }
    }   
    IEnumerator SwordAttack() {
        isAttacking = true;
        //Если на земле тормозимся
        if (groundD.isGrounded)
            rb.velocity = new Vector2(0, 0);

        yield return new WaitForSeconds(swordAttackTime);

        isAttacking = false;
        canMove = true;
        canAttack = true;
    }

    //методы для ивента. Появление\исчезание коллайдера меча
    void SwordAttackColliderStart() {
        rb.WakeUp();
        if (isRightDirection)
            SwordRightCollider.enabled = true;
        //SwordRight.SetActive(true);
        else
            SwordLeftCollider.enabled = true;
        //SwordLeft.SetActive(true);
    }

    void SwordAttackColliderDone() {
        if (isRightDirection)
            SwordRightCollider.enabled = false;
        //SwordRight.SetActive(false);
        else
            SwordLeftCollider.enabled = false;
        //SwordLeft.SetActive(false);
    }

    // Вызывается из анимации 
    void CheckShoot() {
        GameObject spawnedObj = pooler.SpawnFromPool("MagicBall", transform.position, Quaternion.identity);
        MagicBall mb = spawnedObj.GetComponent<MagicBall>();
        mb.SetImpulse(Vector2.right, spriteR.flipX ? -shootForce : shootForce, this);

    }



    #endregion


    #region Move
    void Jump() {
        //if (Input.GetKeyDown(KeyCode.Space) &&  groundD.isGrounded) {
        
        
        if ( groundD.isGrounded) {
            isJumping = true;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("startJump");
        } 
    }

    void Move() {
        direction = Vector3.zero;

        /*
        if (controller.Left.IsPressed) {
            direction = Vector3.left;
        }
        else if (controller.Right.IsPressed) {
            direction = Vector3.right;
        }
         direction *= speed;
        direction.y = rb.velocity.y;
        rb.velocity = direction;
        */

        //Управление джойстиком
        if (joystick.Horizontal >= .2f) {
            direction = Vector3.right * speed;
            direction.y = rb.velocity.y;
            rb.velocity = direction;
        }
        else if (joystick.Horizontal <= -.2f) {
            direction = Vector3.left * speed;
            direction.y = rb.velocity.y;
            rb.velocity = direction;
        }
        else {
            direction = Vector3.zero;
            direction.y = rb.velocity.y;
            rb.velocity = direction;
        }

#if UNITY_EDITOR
        if (useComputerMode) {
            //Debug.Log("useComputerMode");
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

#endif


    }

    void resetHeroPoition() {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
    }

    #endregion


   // public void InitUIController() {
   //     controller = GameManager.Instance.uiConroller;
        //controller.Jump.onClick.AddListener(Jump);
        //controller.Attack.onClick.AddListener(Attack);
        //controller.Fire.onClick.AddListener(Shoot);

  
   // }


   


}
