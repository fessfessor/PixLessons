using System;
using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PlayerLogic
{
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
        [SerializeField] private GameObject SwordObject;   
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
    
    
    
        [HideInInspector]public bool isRightDirection;
        private Vector3 jumpDirection;
        private bool isJumping;
        private bool canMove;
        private bool isMoving;
        private bool isHitted;
        private bool canAttack;
        private Vector3 direction;
        private int currentHealth;
        private bool isAttacking;
        private bool isRolling;
        private bool isLanding;
        private bool isDeath;
        private bool shootReady;
        private ObjectPooler pooler;
        private UIController controller;
        private AudioManager audioManager;

        private Collider2D SwordAttack1Collider;
    
    
        private Image fireButtonImage;
        private float rechargeTimer;    
        private Invulnerability invulnerability;
        private bool jumpButtonEnabled;

        private bool bloodLoss = false;


        private int comboCount = 0;
        private float comboTimer = 0;
        private float slowSpeed;
        private float normarSpeed;

    




        public Health Health { get { return health; } }

        #endregion


        #region AWAKE_AND_INIT_STATE_MACHINE
        private void Awake() {
            //InitStateMachine();
        }
        


        private void InitStateMachine() {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();           
            states = new Dictionary<Type, BaseState>()
            {
                
                {typeof(IdleState), new IdleState(this) },
                {typeof(AttackState), new AttackState(this) },
                {typeof(JumpState), new JumpState(this) },
                {typeof(RollState), new RollState(this) },
                {typeof(MagicAttackState), new MagicAttackState(this) },
                {typeof(PainState), new PainState(this) },
                {typeof(DeathState), new DeathState(this) }
                
            };

            GetComponent<StateMachine>().SetStates(states);
        }
        #endregion


        void Start()
        {
            
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

            SwordAttack1Collider = SwordObject.GetComponent<Collider2D>();
        
            fireButtonImage = fireButton.GetComponent<Image>();

            rechargeTimer = shootRecharge;

            invulnerability = GetComponent<Invulnerability>();

            if (PlayerPrefs.HasKey("jumpButton")) {
                jumpButtonEnabled = PlayerPrefs.GetInt("jumpButton") != 0;
            }
            else {
                jumpButtonEnabled = false;
            }

            slowSpeed = speed / 2;
            normarSpeed = speed;
            // InitUIController();

        }


        void FixedUpdate() {


            SetAnimatorValues();
            CheckFallWithoutJump();
            CheckCharOrientationAndRotate();
            CheckCharacterDeath();
            CheckCharacterPainAndShakeCamera();

            isJumping = !groundD.isGrounded;


            //Движение
            if (!isDeath) {
                if (canMove && !isAttacking ) {
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

                    if (Input.GetKeyDown(KeyCode.F))
                        Roll();
                }
#endif
            }


            if (animator.GetCurrentAnimatorStateInfo(0).IsName("getHit")) {
                speed = 0;
                isHitted = true;
            }
            else {
                speed = normarSpeed;
                isHitted = false;
            }
            

        }


        private void CheckCharOrientationAndRotate()
        {
            if (direction.x > 0)
            {
                isRightDirection = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (direction.x < 0)
            {
                isRightDirection = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }
        }
        private void SetAnimatorValues()
        {
            animator.SetInteger("comboCount", comboCount);        
            animator.SetBool("isGrounded", groundD.isGrounded);
            animator.SetFloat("speed", Mathf.Abs(direction.x));
            animator.SetFloat("isFalling", rb.velocity.y);
        }
        private void CheckFallWithoutJump()
        {
            if (!isJumping && !groundD.isGrounded)
                animator.SetTrigger("fallWithoutJump");
        }
        private void CheckCharacterDeath()
        {
            if (currentHealth <= 0 && !isDeath)
            {
                health.HealthCount = 0;
                dieMenu.SetActive(true);
                isMoving = false;
                isDeath = true;
                AudioManager.Instance.Play("PlayerDie");
                animator.SetTrigger("death");

            }
        }
        private void CheckCharacterPainAndShakeCamera()
        {
            if (isDamaged && ShakeCameraOnDamage && !bloodLoss)
            {
                ProCamera2DShake.Instance.ShakeUsingPreset("PlayerPain");
            }
        }


        private void Update() {
            CheckAttacking();
            CheckShootCooldownAndFillFireButton();
            CheckCharacterIsHittedAndDebuf();

            CheckCharacterGetDamage();

        }

        private void CheckShootCooldownAndFillFireButton()
        {            
            if (rechargeTimer < shootRecharge)
            {
                rechargeTimer += Time.deltaTime;
                fireButtonImage.fillAmount = rechargeTimer / shootRecharge;
            }
            else
                rechargeTimer = shootRecharge;
        }
        private void CheckCharacterGetDamage()
        {
            if (currentHealth > GameManager.Instance.healthContainer[gameObject].HealthCount)
            {
                isDamaged = true;
                currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;

                CheckAndTriggerInvulnerability();
            }
            else
            {
                isDamaged = false;
                currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
            }
        }
        private void CheckAndTriggerInvulnerability()
        {
            if (currentHealth > 0 && !bloodLoss)
            {
                StartCoroutine(invulnerability.GetInvulnerability(true));
                if (!isAttacking && !isJumping)
                    animator.SetTrigger("hit");
            }
        }


        #region Attack

        public void Shoot() {
            if (shootReady && groundD.isGrounded && !isAttacking) 
                StartCoroutine(MagicAttack());          
        }

        IEnumerator MagicAttack() {
            rechargeTimer = 0f;
            animator.SetTrigger("isShooting");       
            shootReady = false;
            canAttack = false;                        
            AudioManager.Instance.Play("BloodLoss");
       
            // Время анимации
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);              
        
            canAttack = true;
            // Время перезарядки
            yield return new WaitForSeconds(shootRecharge - animator.GetCurrentAnimatorStateInfo(0).length);
            shootReady = true;      
        }

    

        public void Attack() {
        
            
            //canAttack = false;                         
            animator.SetTrigger("isSwordAttack");
            StartCoroutine(SwordAttack());
 
           
        }   
        IEnumerator SwordAttack() {

            //canAttack = true;
            isAttacking = true;    
            yield return new WaitForSeconds(1);       
            isAttacking = false;
        
            //BloodLoss(healthLossByHit);
            //canAttack = true;
        }

   
    

        //методы для ивента. Появление\исчезание коллайдера меча. Вызывается из анимации
        void SwordAttackColliderStart() {
            rb.WakeUp();
            SwordAttack1Collider.enabled = true;
        }

        void SwordAttackColliderDone() {
            SwordAttack1Collider.enabled = false;
        }

        // Вызывается из анимации 
        void CheckShoot() {
            GameObject spawnedObj = pooler.SpawnFromPool("MagicBall", transform.position, Quaternion.identity);
            MagicBall mb = spawnedObj.GetComponent<MagicBall>();
            mb.SetImpulse(Vector2.right, shootForce, this);

        }



        #endregion


        #region Move
        private void CharacterStopRolling()
        {
            canMove = true;
            isRolling = false;
            IgnoreEnemyAndTrap(false);
        }

        private void CharacterGetHitDebufStart()
        {                   
         isHitted = true;
        }

        private void CharacterGetHitDebufEnd()
        {
         isHitted = false;
        }

        private void CheckCharacterIsHittedAndDebuf()
        {
            if (isHitted)
            {
                speed = 0;
            }
        }




        public void Jump() {
            isMoving = false;       
            if ( groundD.isGrounded         
                 && !isAttacking
                 && !isHitted         
                 && !isLanding
                 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Falling")

            ) {
                isJumping = true;
                rb.velocity = Vector3.zero;               
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
                animator.SetTrigger("startJump");
            } 
        }


        void Move() {
            direction = Vector3.zero;


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

        //Вызывается кнопкой
        public void Roll() {
            if (!isJumping && !isAttacking) {            
                animator.SetTrigger("roll");
                canMove = false;
                isRolling = true;

                if (isRightDirection)
                {
                    rb.velocity = Vector2.right * 8;
                    IgnoreEnemyAndTrap(true);
                }
                else
                {
                    rb.velocity = Vector2.left * 8;
                    IgnoreEnemyAndTrap(true);
                }
            }
        }

        public void Rolling(bool start) {
            if (start)
                isRolling = true;

        }

        private void IgnoreEnemyAndTrap(bool ignore) {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), ignore);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Traps"), ignore);
        }



        void resetHeroPoition() {
            rb.velocity = new Vector2(0, 0);
            transform.position = new Vector2(0, 0);
        }

        #endregion


       
        void CheckAttacking() {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hero-Attack")
                ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
                ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")
                ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4")
                ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("CastMagic")) {

                isAttacking = true;
                rb.velocity = Vector2.zero;
            } else
                isAttacking = false;
        }

        public void ComboStart(int comboCount) {
            this.comboCount = comboCount;       
        }

        public void ComboEnd() {
            comboCount = 0;
        }

        public void HeroLandStart() {
            isLanding = true;
        }

        public void HeroLandEnd() {
            isLanding = false;
        }

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


        #region SOUND_FROM_ANIMATION
        private void CharacterFootstepsSound()
        {
            AudioManager.Instance.Play("PlayerFootsteps");
        }

        private void CharacterMagicSound()
        {
            AudioManager.Instance.Play("FireballCast");
        }

        private void CharacterSwordAttackSound()
        {
            AudioManager.Instance.Play("SwordSwing");
        }

        private void CharacterJumpingSound()
        {
            AudioManager.Instance.Play("Jump");
        }

        private void CharacterJumpLandingSound()
        {
            AudioManager.Instance.Play("JumpLanding");
        }

        #endregion

    }
}
