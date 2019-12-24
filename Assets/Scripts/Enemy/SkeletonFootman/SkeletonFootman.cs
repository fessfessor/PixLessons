using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonFootman
{
    public class SkeletonFootman : MonoBehaviour, IEnemy
    {

        [Header("Характеристики")]
        //public float TurnFreq = 5f;
        //public float ShootFreq = 3f;
        //public float ShootPower =10f;
        public int attackDamage = 30;
        public float attackFreq = 3f;
        public float speed = 3f;
        public float runSpeed = 4f;
        
        [Header("Ресурсы")]
        public GameObject leftBorderObject;
        public GameObject rightBorderObject;       
             

    
        [Header("Тип")]
        public FootmanType type = FootmanType.SIMPLE;
    
        
        [HideInInspector]public Vector3 leftBorderPosition;
        [HideInInspector]public Vector3 rightBorderPosition;
        [HideInInspector]public GameObject enemy;
        [HideInInspector]public bool inAgrArea;
        [HideInInspector]public bool inAttackArea;
        [HideInInspector]public Animator anim;
        [HideInInspector]public Rigidbody2D rb;
        [HideInInspector]public bool isRightDirection = true;
        [HideInInspector]public bool isDeath;
        [HideInInspector]public bool endLooking;
    
        private void Awake()
        {
            InitStateMachine();
        }


        // В зависимости от типа скелета он умеет делать разные штуки
        private void InitStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            if (type == FootmanType.SIMPLE)
            {
                states = new Dictionary<Type, BaseState>()
                {
                    {typeof(WatchingState), new WatchingState(this) },
                    {typeof(AttackingState), new AttackingState(this) },
                    {typeof(ChasingState), new ChasingState(this) }

                };
            }
            else if (type == FootmanType.BLOCKING) 
            {
                states = new Dictionary<Type, BaseState>()
                {
                    {typeof(WatchingState), new WatchingState(this) },
                    {typeof(AttackingState), new AttackingState(this) },
                    {typeof(ChasingState), new ChasingState(this) },
                    {typeof(BlockingState), new BlockingState(this) }
                    
                };
            }


            GetComponent<StateMachine>().SetStates(states);
        }
    
        // Start is called before the first frame update
        private void Start()
        {
        
            //pooler = ObjectPooler.Instance;
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            enemy = GameManager.Instance.player;
            leftBorderPosition = leftBorderObject.transform.position;
            rightBorderPosition = rightBorderObject.transform.position;



        }
        
        private void Update()
        {
            if (GameManager.Instance.healthContainer[gameObject].HealthCount <= 0 && !isDeath)
                Death();

            anim.SetBool("inAgrArea", inAgrArea);
        }

        

        public void Attack(bool isAttacking, GameObject enemy, GameObject sender = null)
        {
            if (sender.transform.name == "AgrArea")
            {
                inAgrArea = isAttacking;
                //this.enemy = enemy;
            }

            if (sender.transform.name == "AttackArea")
            {
                inAttackArea = isAttacking;
                //this.enemy = enemy;
            }
        }

        public void SwitchVision() {
            transform.rotation = Quaternion.Euler(0, transform.rotation == Quaternion.Euler(0, 0, 0) ? 180 : 0, 0);
            isRightDirection = transform.rotation.y <= 0;

        }

        public void Death()
        {
            isDeath = true;
            anim.SetTrigger("Death");
        }

        public void Move()
        {
            
        }

        public void Damage()
        {
            if (inAttackArea && enemy != null) {
                GameManager.Instance.healthContainer[enemy].takeHit(attackDamage);
            }
        }

        //Animation event
        public void AttackImpulse()
        {
            if(isRightDirection)
                rb.AddForce(Vector2.right * 35, ForceMode2D.Impulse);
            else
                rb.AddForce(Vector2.left * 35, ForceMode2D.Impulse);
        }
    
        public enum FootmanType
        {
            SIMPLE, BLOCKING
        }

        
    }
}
