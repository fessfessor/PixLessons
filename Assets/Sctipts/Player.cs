﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Com.LuisPedroFonseca.ProCamera2D;

public class Player : MonoBehaviour
{
    #region variables
    [Header("Физические параметры")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float force = 1.0f;
    [SerializeField] private float shootRecharge = 3.0f;   
    [SerializeField] private float minHeight = -50.0f;
    [SerializeField] private float swordAttackTime;
    [SerializeField] private float shootForce;

    [Header("Компоненты")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GroundDetection groundD;   
    [SerializeField] private SpriteRenderer spriteR;   
    [SerializeField] private Animator animator;
    [SerializeField] private CameraShake cameraShaker;
    [SerializeField] private MagicBall magicBall;
    [SerializeField] private Health health;
    [SerializeField] private GameObject SwordRight;
    [SerializeField] private GameObject SwordLeft;
    [SerializeField] private GameObject fireButton;
    [SerializeField] private GameObject dieMenu;
    [SerializeField] private Joystick joystick;

    [Header("События при дамаге")]
    [SerializeField] private bool ShakeCameraOnDamage;
    [SerializeField] private bool isDamaged;
    public bool IsDamaged { get => isDamaged; }

    [Header("Компьютерный мод")]
    [SerializeField] bool useComputerMode;

    [Header("Механика кровавого меча")]
    [SerializeField] private int healthLossByHit; 
    [SerializeField] private int healthLossByShoot;
    [SerializeField] private int healthReturnByHit; 
    [SerializeField] private int healthReturnByShoot; 
    
    
    
    private  bool isRightDirection;
    private Vector3 jumpDirection;
    private bool isJumping;
    private bool canMove;
    private bool isMoving;
    private bool canAttack;
    private Vector3 direction;
    private int currentHealth;
    private bool isAttacking;
    private bool isDeath;
    private bool shootReady;
    private ObjectPooler pooler;
    private UIController controller;
    private AudioManager audioManager;

    private Collider2D SwordRightCollider;
    private Collider2D SwordLeftCollider;
    
    private Image fireButtonImage;
    private float rechargeTimer;    
    private Invulnerability invulnerability;
    private bool jumpButtonEnabled;

    private bool bloodLoss = false;

    




    public Health Health { get { return health; } }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log($"Speed : {speed} Forse : {force} ShootRechardge: {shootRecharge}");

        //Подписываемся на события "кровавой механики"
        EventManager.Instance.AddListener(EVENT_TYPE.BLD_BALL_HIT, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.BLD_BALL_MISS, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.BLD_MELEE_HIT, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.BLD_MELEE_MISS, OnEvent);
        

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
        fireButtonImage = fireButton.GetComponent<Image>();

        rechargeTimer = shootRecharge;

        invulnerability = GetComponent<Invulnerability>();

        if (PlayerPrefs.HasKey("jumpButton")) {
            jumpButtonEnabled = PlayerPrefs.GetInt("jumpButton") == 0 ? false : true;
        }
        else {
            jumpButtonEnabled = false;
        }
        

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
        if (vetricalMove >= .5f && !jumpButtonEnabled) {
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
        //Таймер перезарядки выстрела
        if (rechargeTimer < shootRecharge) {
            rechargeTimer += Time.deltaTime;
            fireButtonImage.fillAmount = rechargeTimer / shootRecharge;
        }
        else
            rechargeTimer = shootRecharge;



        // Проверка на дамаг
        if (currentHealth > GameManager.Instance.healthContainer[gameObject].HealthCount) {
            isDamaged = true; 
            currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
            if(currentHealth > 0 && !bloodLoss) {
                StartCoroutine(invulnerability.GetInvulnerability());
                AudioManager.Instance.Play("Pain");
            }
                
        }
        else {
            isDamaged = false;
            currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
        }

        // Гибель
        if(currentHealth <= 0 && !isDeath) {
            Death();
        }

        //При уменьшении здоровья трясем камеру
        if (isDamaged && ShakeCameraOnDamage && !bloodLoss) {
            ProCamera2DShake.Instance.ShakeUsingPreset("PlayerPain");
            //cameraShaker.Shake();
        }
            
        
        // Звуки шагов
        if (isMoving && !AudioManager.Instance.isPlaying("PlayerFootsteps") && groundD.isGrounded) {
            AudioManager.Instance.Play("PlayerFootsteps");
        }
        else if((!isMoving || !groundD.isGrounded) && AudioManager.Instance.isPlaying("PlayerFootsteps")) {
            AudioManager.Instance.Stop("PlayerFootsteps");
        }

            
    }

    
    void Death() {
        health.HealthCount = 0;
        dieMenu.SetActive(true);
        isMoving = false;
        isDeath = true;
        AudioManager.Instance.Play("PlayerDie");
        Time.timeScale = 0.1f;
    }


    #region Attack

    public void Shoot() {
        if (shootReady && groundD.isGrounded && !isAttacking) 
            StartCoroutine(MagicAttack());          
    }

    IEnumerator MagicAttack() {
        rechargeTimer = 0f;
        animator.SetTrigger("isShooting");
        isAttacking = true;
        shootReady = false;
        canAttack = false;
        canMove = false;
        rb.velocity = Vector2.zero;
        AudioManager.Instance.Play("FireballCast");
        AudioManager.Instance.Play("BloodLoss");
        //За выстрел платим здоровьем
        //HealthChange(healthByShoot, true);
        
        // Время анимации
        yield return new WaitForSeconds(0.8f);        
        isAttacking = false;
        canMove = true;
        canAttack = true;
        // Время перезарядки
        yield return new WaitForSeconds(shootRecharge - 0.8f);
        shootReady = true;      
    }

    

    public void Attack() {
        if (!isAttacking) {
            canAttack = false;
            canMove = false;
            AudioManager.Instance.Play("SwordSwing");
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
        BloodLoss(healthLossByHit);
        isAttacking = false;
        canMove = true;
        canAttack = true;
    }

    //методы для ивента. Появление\исчезание коллайдера меча
    void SwordAttackColliderStart() {
        rb.WakeUp();
        if (isRightDirection) {
            SwordRightCollider.enabled = true;            
            
        }
        else {
            SwordLeftCollider.enabled = true;
            //numberHits = SwordLeftCollider.Cast(Vector2.left, hits);
            //Debug.Log("Left - " + numberHits + " " + hits[0].transform.name + " " + hits[1].transform.name);
        }
            
        
    }

    void SwordAttackColliderDone() {
        if (isRightDirection)
            SwordRightCollider.enabled = false;
        else
            SwordLeftCollider.enabled = false;

    }

    // Вызывается из анимации 
    void CheckShoot() {
        GameObject spawnedObj = pooler.SpawnFromPool("MagicBall", transform.position, Quaternion.identity);
        MagicBall mb = spawnedObj.GetComponent<MagicBall>();
        mb.SetImpulse(Vector2.right, spriteR.flipX ? -shootForce : shootForce, this);

    }



    #endregion


    #region Move
    public void Jump() {
        isMoving = false;
        //if (Input.GetKeyDown(KeyCode.Space) &&  groundD.isGrounded) {


        if ( groundD.isGrounded) {
            isJumping = true;
            rb.velocity = Vector3.zero;
            AudioManager.Instance.Play("Jump");
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetTrigger("startJump");
        } 
    }

    // Звук приземления , вызывается из анимации
    void JumpLanding() {
        AudioManager.Instance.Play("JumpLanding");
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
            if(!isJumping)
                isMoving = true;
        }
        else if (joystick.Horizontal <= -.2f) {
            direction = Vector3.left * speed;
            direction.y = rb.velocity.y;
            rb.velocity = direction;
            if (!isJumping)
                isMoving = true;
        }
        else {
            direction = Vector3.zero;
            direction.y = rb.velocity.y;
            rb.velocity = direction;
            isMoving = false;
            
        }

#if UNITY_EDITOR
        if (useComputerMode) {
            //Debug.Log("useComputerMode");
            if (Input.GetKey(KeyCode.A)) {
                direction = Vector3.left;
                if (!isJumping)
                    isMoving = true;
            }
            else if (Input.GetKey(KeyCode.D)) {
                direction = Vector3.right;
                if (!isJumping)
                    isMoving = true;
            }
            else {
                isMoving = false;
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
    // 



    #region bloodMechanics
    // Механика "кровавых атак". 
    

    private void BloodLoss(int healthCount) {
        health.HealthCount -= healthCount;
        StartCoroutine(bloodDelay());
        
    }

    private void BloodVampire(int healthCount) {
        health.HealthCount += healthCount;
    }
   
    IEnumerator bloodDelay() {
        bloodLoss = true;
        for (int i = 0; i < 2; i++) {
            yield return null;
        }
        bloodLoss = false;
    }

    #endregion

    #region events
    void OnEvent(EVENT_TYPE eventType, Component sender, object param = null) {
        switch (eventType) {            
            case EVENT_TYPE.BLD_BALL_HIT:
                BloodVampire(healthReturnByShoot);
                break;
            case EVENT_TYPE.BLD_BALL_MISS:
                BloodLoss(healthLossByShoot);
                break;
            case EVENT_TYPE.BLD_MELEE_HIT:
                BloodVampire(healthReturnByHit);
                break;
            case EVENT_TYPE.BLD_MELEE_MISS:
                BloodLoss(healthLossByHit);
                break;          
            default:
                break;
        }
    }

    #endregion







}
