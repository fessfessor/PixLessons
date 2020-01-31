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
                {typeof(FireAttackState), new FireAttackState(this) }

            };


            GetComponent<StateMachine>().SetStates(states);
        }

       
        void Start()
        {

        }


        void Update()
        {

        }

        
    }
}