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
        public float Speed { get { return speed; } set { speed = value; } }
        [SerializeField] private float force = 1.0f;
        public float Force { get => force; set => force = value; }
        [SerializeField] private float shootRecharge = 3.0f;
        [SerializeField] private float minHeight = -50.0f;
        [SerializeField] private float swordAttackTime;
        [SerializeField] private float shootForce;

        [Header("Компоненты")]

        [SerializeField] private SpriteRenderer spriteR;


        [SerializeField] private CameraShake cameraShaker;
        [SerializeField] private MagicBall magicBall;
        [SerializeField] private Health health;
        [SerializeField] private GameObject SwordObject;
        [SerializeField] private GameObject fireButton;

        [SerializeField] private Joystick joystick;
        public Joystick Joystick
        {
            get { return joystick; }
        }

        [Header("События при дамаге")]
        [SerializeField] private bool ShakeCameraOnDamage;
        [SerializeField] private bool isDamaged;
        public bool IsDamaged { get => isDamaged; }

        [Header("Компьютерный мод")]
        [SerializeField] bool useComputerMode;
        public bool UseComputerMode
        {
            get { return useComputerMode; }
            set { useComputerMode = value; }
        }

        [Header("Механика кровавого меча")]
        [SerializeField] private int healthLossByHit;
        [SerializeField] private int healthLossByShoot;
        [SerializeField] private int healthReturnByHit;
        [SerializeField] private int healthReturnByShoot;







        private Vector3 jumpDirection;
        //private bool isJumping;
        private bool canMove;
        private bool isMoving;
        private bool isHited;
        private bool canAttack;
        private Vector3 direction;
        private int currentHealth;
        private bool isAttacking;
        private bool isRolling;

        private bool isDeath;
        private bool shootReady;
        private ObjectPooler pooler;
        private UIController controller;
        private AudioManager audioManager;

        private Collider2D SwordAttack1Collider;


        private Image fireButtonImage;
        private float rechargeTimer;
        private Invulnerability invulnerability;


        private bool bloodLoss = false;


        private int comboCount = 0;
        private float comboTimer = 0;
        private float slowSpeed;
        private float normarSpeed;






        public Health Health { get { return health; } }

        private Animator animator;
        public Animator Animator { get => animator; set => animator = value; }

        private bool isRightDirection;
        public bool IsRightDirection { get => isRightDirection; set => isRightDirection = value; }

        private bool jumpButtonEnabled;
        public bool JumpButtonEnabled { get => jumpButtonEnabled; set => jumpButtonEnabled = value; }

        private Rigidbody2D rb;
        public Rigidbody2D Rb { get => rb; set => rb = value; }

        private GroundDetection groundD;
        public GroundDetection GroundD { get => groundD; set => groundD = value; }

        private bool isJumping;
        public bool IsJumping { get => isJumping; set => isJumping = value; }




        #endregion



        private void Awake()
        {
            InitStateMachine();
        }


        private void InitStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            states = new Dictionary<Type, BaseState>()
            {

                {typeof(IdleState), new IdleState(this) },
                {typeof(RunState), new RunState(this) },
                {typeof(AttackState), new AttackState(this) },
                {typeof(JumpState), new JumpState(this) },
                {typeof(RollState), new RollState(this) },
                {typeof(MagicAttackState), new MagicAttackState(this) },
                {typeof(PainState), new PainState(this) },
                {typeof(DeathState), new DeathState(this) }

            };

            GetComponent<StateMachine>().SetStates(states);
        }



        void Start()
        {

            //Подписываемся на события "кровавой механики"
            EventManager.Instance.AddListener(EVENT_TYPE.BLD_BALL_HIT, OnEvent);
            EventManager.Instance.AddListener(EVENT_TYPE.BLD_BALL_MISS, OnEvent);
            EventManager.Instance.AddListener(EVENT_TYPE.BLD_MELEE_HIT, OnEvent);
            EventManager.Instance.AddListener(EVENT_TYPE.BLD_MELEE_MISS, OnEvent);


            pooler = ObjectPooler.Instance;
            audioManager = AudioManager.Instance;
            animator = GetComponent<Animator>();
            groundD = GetComponent<GroundDetection>();
            rb = GetComponent<Rigidbody2D>();

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

            if (PlayerPrefs.HasKey("jumpButton"))
            {
                jumpButtonEnabled = PlayerPrefs.GetInt("jumpButton") != 0;
            }
            else
            {
                jumpButtonEnabled = false;
            }

            slowSpeed = speed / 2;
            normarSpeed = speed;

        }


        void FixedUpdate()
        {


            animator.SetInteger("comboCount", comboCount);
            animator.SetBool("isGrounded", groundD.isGrounded);

            CheckFallingAndSetTrigger();



            //Движение
            if (!isDeath)
            {


#if UNITY_EDITOR
                if (useComputerMode)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0) && canAttack)
                        Attack();


                    if (shootReady && Input.GetMouseButtonDown(1) && groundD.isGrounded && !isAttacking)
                        Shoot();


                    if (Input.GetKeyDown(KeyCode.F))
                        Roll();
                }
#endif
            }



            //Перекат
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Roll"))
            {
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
            else
            {
                canMove = true;
                isRolling = false;
                IgnoreEnemyAndTrap(false);
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("getHit"))
            {
                speed = 0;
                isHited = true;
            }
            else
            {
                speed = normarSpeed;
                isHited = false;
            }


        }




        private void Update()
        {
            CheckAttacking();


            //Таймер перезарядки выстрела
            if (rechargeTimer < shootRecharge)
            {
                rechargeTimer += Time.deltaTime;
                fireButtonImage.fillAmount = rechargeTimer / shootRecharge;
            }
            else
                rechargeTimer = shootRecharge;



            // Проверка на дамаг
            if (currentHealth > GameManager.Instance.healthContainer[gameObject].HealthCount)
            {
                isDamaged = true;
                currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
                if (currentHealth > 0 && !bloodLoss)
                {
                    StartCoroutine(invulnerability.GetInvulnerability(true));
                    AudioManager.Instance.Play("Pain");
                    if (!isAttacking && !isJumping)
                        animator.SetTrigger("hit");

                }

            }
            else
            {
                isDamaged = false;
                currentHealth = GameManager.Instance.healthContainer[gameObject].HealthCount;
            }


            //При уменьшении здоровья трясем камеру
            if (isDamaged && ShakeCameraOnDamage && !bloodLoss)
            {
                ProCamera2DShake.Instance.ShakeUsingPreset("PlayerPain");
            }




            // Звуки шагов
            if (isMoving && !AudioManager.Instance.isPlaying("PlayerFootsteps") && groundD.isGrounded)
            {
                AudioManager.Instance.Play("PlayerFootsteps");
            }
            else if ((!isMoving || !groundD.isGrounded) && AudioManager.Instance.isPlaying("PlayerFootsteps"))
            {
                AudioManager.Instance.Stop("PlayerFootsteps");
            }


        }



        #region Attack

        public void Shoot()
        {
            if (shootReady && groundD.isGrounded && !isAttacking)
                StartCoroutine(MagicAttack());
        }

        IEnumerator MagicAttack()
        {
            rechargeTimer = 0f;
            animator.SetTrigger("isShooting");
            shootReady = false;
            canAttack = false;
            AudioManager.Instance.Play("FireballCast");
            AudioManager.Instance.Play("BloodLoss");

            // Время анимации
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

            canAttack = true;
            // Время перезарядки
            yield return new WaitForSeconds(shootRecharge - animator.GetCurrentAnimatorStateInfo(0).length);
            shootReady = true;
        }



        public void Attack()
        {
            //AudioManager.Instance.Play("SwordSwing");
            animator.SetTrigger("isSwordAttack");
            StartCoroutine(SwordAttack());

        }
        IEnumerator SwordAttack()
        {
            isAttacking = true;
            yield return new WaitForSeconds(1);
            isAttacking = false;
        }




        //методы для ивента. Появление\исчезание коллайдера меча. Вызывается из анимации
        void SwordAttackColliderStart()
        {
            rb.WakeUp();
            SwordAttack1Collider.enabled = true;
        }

        void SwordAttackColliderDone()
        {
            SwordAttack1Collider.enabled = false;
        }

        // Вызывается из анимации 
        void CheckShoot()
        {
            GameObject spawnedObj = pooler.SpawnFromPool("MagicBall", transform.position, Quaternion.identity);
            MagicBall mb = spawnedObj.GetComponent<MagicBall>();
            mb.SetImpulse(Vector2.right, shootForce, this);

        }



        #endregion


        #region Move


        // Звук приземления , вызывается из анимации
        void JumpLanding()
        {
            AudioManager.Instance.Play("JumpLanding");
        }



        //Вызывается кнопкой
        public void Roll()
        {
            if (!isJumping && !isAttacking)
            {
                animator.SetTrigger("roll");
            }
        }

        public void Rolling(bool start)
        {
            if (start)
                isRolling = true;

        }

        private void IgnoreEnemyAndTrap(bool ignore)
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Enemy"), ignore);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Traps"), ignore);
        }
 

        #endregion



        void CheckAttacking()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hero-Attack")
                ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2")
                ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3")
                ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("Attack4")
                ||
                animator.GetCurrentAnimatorStateInfo(0).IsName("CastMagic"))
            {

                isAttacking = true;
                rb.velocity = Vector2.zero;
            }
            else
                isAttacking = false;
        }

        public void ComboStart(int comboCount)
        {
            this.comboCount = comboCount;
        }

        public void ComboEnd()
        {
            comboCount = 0;
        }


        public void CharacterStartJump()
        {
            isJumping = true;            
            //EventManager.Instance.PostNotification(EVENT_TYPE.PLAYER_START_JUMP, this);
        }

        public void CharacterEndJump()
        {
            isJumping = false;            
            //EventManager.Instance.PostNotification(EVENT_TYPE.PLAYER_END_JUMP, this);
        }

        #region bloodMechanics
        // Механика "кровавых атак". 


        private void BloodLoss(int healthCount)
        {
            health.HealthCount -= healthCount;
            StartCoroutine(bloodDelay());

        }

        private void BloodVampire(int healthCount)
        {
            health.HealthCount += healthCount;
        }

        IEnumerator bloodDelay()
        {
            bloodLoss = true;
            for (int i = 0; i < 2; i++)
            {
                yield return null;
            }
            bloodLoss = false;
        }

        #endregion

        #region events
        void OnEvent(EVENT_TYPE eventType, Component sender, object param = null)
        {
            switch (eventType)
            {
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



        private void CheckFallingAndSetTrigger()
        {
            if (!groundD.isGrounded)
                animator.SetTrigger("fallWithoutJump");
        }



        public bool CharacterPressRunning()
        {
#if UNITY_EDITOR
            if (useComputerMode)
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                {
                    return true;
                }

            }
#endif
            if (joystick.Horizontal >= .2f || joystick.Horizontal <= -.2f)
            {
                return true;
            }

            return false;

        }

        public bool CharacterPressJumping()
        {

            float vetricalMove = joystick.Vertical;
            if (vetricalMove >= .5f && !jumpButtonEnabled)
            {
                return true;
            }

#if UNITY_EDITOR
            if (useComputerMode)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    return true;
                }

            }
#endif

            return false;
        }

        public bool CharacterPressRolling()
        {


#if UNITY_EDITOR
            if (useComputerMode)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    return true;
                }

            }
#endif
            return false;

        }

    }

}
