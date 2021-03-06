﻿using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{
    public class MeleeAttackState : BaseState
    {
        SkeletonDragonBoss _dragon;
        
        private float cooldownTimer = 0f;
       

        public MeleeAttackState(SkeletonDragonBoss _dragon) : base(_dragon.gameObject)
        {
            this._dragon = _dragon;
            
        }

        public override Type Tick()
        {

             if (_dragon.PlayerInLongArea && !_dragon.PlayerInShortArea)
            {
                _dragon.DragonAnimator.SetBool("MeleeAttackBool", false);
                return typeof(FireAttackState);
            }
                

            // if (_dragon.PlayerInBackArea)
            //     return typeof(FlyState);
            MeleeAttack();

            if (!_dragon.PlayerInBackArea && !_dragon.PlayerInLongArea && !_dragon.PlayerInShortArea)
                return typeof(IdleState);

            return null;
        }



        private void MeleeAttack()
        {
            cooldownTimer += Time.deltaTime;

            if (_dragon.StateMachine.PreviousState != typeof(MeleeAttackState))
            {
                cooldownTimer = 0f;
                _dragon.DragonAnimator.SetBool("MeleeAttackBool", true);

            }
            else
            {
                if(cooldownTimer > _dragon.attackFreq)
                {
                    cooldownTimer = 0f;
                    _dragon.DragonAnimator.SetBool("MeleeAttackBool", true);
                }
            }


            
        }

        


    }
}