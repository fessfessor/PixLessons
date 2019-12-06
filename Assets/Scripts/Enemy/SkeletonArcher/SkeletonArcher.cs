using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonArcher
{
    public class SkeletonArcher : MonoBehaviour, IEnemy
    {
        [Header("Характеристики")]
        public float TurnFreq = 5f;
        public float ShootFreq = 3f;
        public float ShootPower =10f;
        public int attackDamage = 30;
        public float attackFreq = 3f;
        public float speed = 3f;

        [Header("Тип")]
        public ArcherType type = ArcherType.SIMPLE_SHOOT;

        [Header("Ресурсы")]
        public GameObject arrowPrefab;
        public GameObject leftBorderObject;
        public GameObject rightBorderObject;

        [HideInInspector]public Vector3 leftBorderPosition;
        [HideInInspector]public Vector3 rightBorderPosition;
        [HideInInspector]public GameObject enemy;
        [HideInInspector]public bool inShootArea;
        [HideInInspector]public bool inAttackArea;
        [HideInInspector]public Animator anim;
        [HideInInspector]public Rigidbody2D rb;
        [HideInInspector]public bool isRightDirection = true;
        [HideInInspector] public bool isDeath;

        private int currentHealth;
        private GameObject arrow;
        //private ObjectPooler pooler;




        private void Awake()
        {
            InitStateMachine();
        }


        // В зависимости от типа скелета он умеет делать разные штуки
        private void InitStateMachine()
        {
            Dictionary<Type, BaseState> states = new Dictionary<Type, BaseState>();
            if (type == ArcherType.SIMPLE_SHOOT)
            {
                states = new Dictionary<Type, BaseState>()
                {
                    {typeof(WatchingState), new WatchingState(this) },
                    {typeof(ShootingState), new ShootingState(this) }

                };
            }
            else if (type == ArcherType.SHOOT_AND_HIT) 
            {
                states = new Dictionary<Type, BaseState>()
                {
                    {typeof(WatchingState), new WatchingState(this) },
                    {typeof(ShootingState), new ShootingState(this) },
                    {typeof(HittingState), new HittingState(this) }
                };
            }
            else if (type == ArcherType.PATROL)
            {
                states = new Dictionary<Type, BaseState>()
                {
                    {typeof(PatrolState), new PatrolState(this) },
                    {typeof(ShootingState), new ShootingState(this) },
                    {typeof(HittingState), new HittingState(this) }
                
                };
            }


            GetComponent<StateMachine>().SetStates(states);
        }
    


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


        }

        public void SwitchVision() {
            transform.rotation = Quaternion.Euler(0, transform.rotation == Quaternion.Euler(0, 0, 0) ? 180 : 0, 0);
            isRightDirection = transform.rotation.y <= 0;

        }


        //Информация из дочернего объекта(область стрельбы, атаки) с коллайдером
        public void Attack(bool isAttacking, GameObject enemy, GameObject sender)
        {
            if (sender.transform.name == "ShootArea")
            {
                inShootArea = isAttacking;
                //this.enemy = enemy;
            }

            if (sender.transform.name == "AttackArea")
            {
                inAttackArea = isAttacking;
                //this.enemy = enemy;
            }
        

        }

        public void Death()
        {
            isDeath = true;
            anim.SetTrigger("Death");
        }

        public void Move()
        {
       
        }

    
        // Вызывается из анимации
        public void Damage()
        {
            if (inAttackArea && enemy!=null) {
                GameManager.Instance.healthContainer[enemy].takeHit(attackDamage);
            }
        
        }


        //Вызывается из анимации
        public void Shoot()
        {
        
        
//        arrow = pooler.SpawnFromPool("Arrow", transform.position, Quaternion.identity);
//       if(!isRightDirection)
//           arrow.transform.rotation = Quaternion.Euler(0,180,0);
//       else
//           arrow.transform.rotation = Quaternion.Euler(0,0,0);

            if (enemy != null)
            {
                arrow = Instantiate(arrowPrefab, transform.position,
                    !isRightDirection ? Quaternion.Euler(0, 180, 0) : Quaternion.identity);
            
                arrow.GetComponent<Rigidbody2D>().AddForce((enemy.transform.position - transform.position).normalized * ShootPower, ForceMode2D.Impulse);
            }
            
        }

        public enum ArcherType
        {
            SIMPLE_SHOOT, SHOOT_AND_HIT, PATROL
        }

    
    }
}



