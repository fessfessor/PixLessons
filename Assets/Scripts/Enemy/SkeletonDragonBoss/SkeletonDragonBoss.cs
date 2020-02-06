using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{

    public class SkeletonDragonBoss : MonoBehaviour
    {


        [Header("Характеристики")]
        public float TurnDelay = 2f;
        public float ShootFreq = 3f;
        public float ShootPower = 10f;
        public int attackDamage = 30;
        public float attackFreq = 3f;
        public float fireAttackFreq = 3f;
        public float fireAttackForce = 3f;
        public float fireAttackDamage = 3f;
        public float speed = 3f;


        [Header("Ресурсы")]
        public GameObject fireBallPrefab;
        public GameObject fireballSpawnPoint;

        private bool isRightDirection = false;
        public bool IsRightDirection { get => isRightDirection; set => isRightDirection = value; }

        private bool playerInLongArea = false;
        public bool PlayerInLongArea { get => playerInLongArea; set => playerInLongArea = value; }

        private bool playerInShortArea = false;
        public bool PlayerInShortArea { get => playerInShortArea; set => playerInShortArea = value; }

        private bool playerInBackArea = false;
        public bool PlayerInBackArea { get => playerInBackArea; set => playerInBackArea = value; }

        private bool playerInWalkArea = false;
        public bool PlayerInWalkArea { get => playerInWalkArea; set => playerInWalkArea = value; }

        private Animator dragonAnimator;
        public Animator DragonAnimator { get => dragonAnimator; set => dragonAnimator = value; }
        
        private StateMachine stateMachine;
        public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
        
        private Rigidbody2D rb;
        public Rigidbody2D Rb { get => rb; set => rb = value; }

        private float flyTimer = 0f;
        public float FlyTimer { get => flyTimer; set => flyTimer = value; }

        private GameObject player;
        private ObjectPooler pooler;
        
        
       

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
                {typeof(MeleeAttackState), new MeleeAttackState(this) },
                {typeof(FlyState), new FlyState(this) },
                {typeof(DeathState), new DeathState(this) },
                {typeof(FireAttackState), new FireAttackState(this) },
                {typeof(WalkState), new WalkState(this) }

            };


            GetComponent<StateMachine>().SetStates(states);
        }

       
        void Start()
        {
            dragonAnimator = GetComponent<Animator>();
            stateMachine = GetComponent<StateMachine>();
            rb = GetComponent<Rigidbody2D>();
            player = GameManager.Instance.player;
            pooler = ObjectPooler.Instance;
            

            StartCoroutine(CheckAndSwitchVision());
        }


        void Update()
        {
            flyTimer += Time.deltaTime;
        }




        IEnumerator CheckAndSwitchVision()
        {
            while (true)
            {
                
                if (player.transform.position.x > transform.position.x
                    && !IsRightDirection)
                {
                    
                    yield return new WaitForSeconds(TurnDelay);                   
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    IsRightDirection = true;
                    yield return null;
                }

                if (player.transform.position.x < transform.position.x
                    && IsRightDirection)
                {
                    
                    yield return new WaitForSeconds(TurnDelay);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    IsRightDirection = false;
                    yield return null;
                }

                yield return null;

            }


        }

        //Animation event
        private void FireAttack()
        {
            var fireball = pooler.SpawnFromPool("DragonFireball", fireballSpawnPoint.transform.position, Quaternion.identity);
            var direction = isRightDirection ? Vector2.right : Vector2.left;
            fireball.GetComponent<Rigidbody2D>().AddForce(direction * fireAttackForce, ForceMode2D.Impulse);
        }

        //Animation event
        private void MeleeAttack()
        {

        }


        public bool PlayerInAreas()
        {
            return PlayerInBackArea || PlayerInLongArea || PlayerInShortArea || PlayerInWalkArea;
        }




        






    }
}