using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{

    public class SkeletonDragonBoss : MonoBehaviour
    {


        [Header("Характеристики")]
        public float TurnFreq = 5f;
        public float ShootFreq = 3f;
        public float ShootPower = 10f;
        public int attackDamage = 30;
        public float attackFreq = 3f;
        public float speed = 3f;


        [Header("Ресурсы")]
        public GameObject fireBallPrefab;


        private bool playerInLongArea = false;
        public bool PlayerInLongArea { get => playerInLongArea; set => playerInLongArea = value; }

        private bool playerInShortArea = false;
        public bool PlayerInShortArea { get => playerInShortArea; set => playerInShortArea = value; }

        private bool playerInBackArea = false;
        public bool PlayerInBackArea { get => playerInBackArea; set => playerInBackArea = value; }
        

        private Animator dragonAnimator;
        public Animator DragonAnimator { get => dragonAnimator; set => dragonAnimator = value; }


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
        }


        void Update()
        {

        }



        
    }
}