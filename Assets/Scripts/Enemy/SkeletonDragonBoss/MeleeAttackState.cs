using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{
    public class MeleeAttackState : BaseState
    {
        SkeletonDragonBoss _dragon;
        private Animator dragonAnimator;
        private float cooldownTimer;
        public MeleeAttackState(SkeletonDragonBoss _dragon) : base(_dragon.gameObject)
        {
            this._dragon = _dragon;
            dragonAnimator = _dragon.gameObject.GetComponent<Animator>();
        }

        public override Type Tick()
        {
            cooldownTimer += Time.deltaTime;


            // if (_dragon.PlayerInLongArea)
            //    return typeof(FireAttackState);

            // if (_dragon.PlayerInBackArea)
            //     return typeof(FlyState);
            MeleeAttack();

            if (!_dragon.PlayerInBackArea && !_dragon.PlayerInLongArea && !_dragon.PlayerInShortArea)
                return typeof(IdleState);

            return null;
        }



        private void MeleeAttack()
        {
            if (PreviousState != typeof(MeleeAttackState))
            {
                _dragon.DragonAnimator.SetTrigger("MeleeAttackTrigger");
            }
            else
            {
                if(cooldownTimer > _dragon.attackFreq)
                {
                    _dragon.DragonAnimator.SetTrigger("MeleeAttackTrigger");
                }
            }


            
        }

        


    }
}