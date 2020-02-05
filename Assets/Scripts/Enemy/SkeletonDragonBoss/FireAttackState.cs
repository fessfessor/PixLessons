using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{
    public class FireAttackState : BaseState
    {
        private float cooldownTimer = 0f;
        SkeletonDragonBoss _dragon;
        public FireAttackState(SkeletonDragonBoss _dragon) : base(_dragon.gameObject)
        {
            this._dragon = _dragon;
        }

        public override Type Tick()
        {
            if(_dragon.FlyTimer > 10)
            {
                _dragon.DragonAnimator.SetBool("FireAttackBool", false);
                return typeof(FlyState);
            }

            FireAttack();

            if (_dragon.PlayerInWalkArea)
            {
                _dragon.DragonAnimator.SetBool("FireAttackBool", false);
                return typeof(WalkState);
            }

            if (_dragon.PlayerInShortArea)
            {
                _dragon.DragonAnimator.SetBool("FireAttackBool", false);
                return typeof(MeleeAttackState);
            }
            




            return null;
        }

        private void FireAttack()
        {
            cooldownTimer += Time.deltaTime;

            if (_dragon.StateMachine.PreviousState != typeof(FireAttackState))
            {
                cooldownTimer = 0f;
                _dragon.DragonAnimator.SetBool("FireAttackBool", true);

            }
            else
            {
                if (cooldownTimer > _dragon.fireAttackFreq)
                {
                    cooldownTimer = 0f;
                    _dragon.DragonAnimator.SetBool("FireAttackBool", true);
                }
            }
        }


    }
}