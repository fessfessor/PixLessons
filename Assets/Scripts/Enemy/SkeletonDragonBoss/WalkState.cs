using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{
    public class WalkState : BaseState
    {
        SkeletonDragonBoss _dragon;
        public WalkState(SkeletonDragonBoss _dragon) : base(_dragon.gameObject)
        {
            this._dragon = _dragon;
        }

        public override Type Tick()
        {
            
            
            if (_dragon.PlayerInWalkArea)
            {
                Walk();
            }

            if (_dragon.PlayerInLongArea)
            {
                _dragon.DragonAnimator.SetBool("WalkingBool", false);
                return typeof(FireAttackState);
            }

            if (_dragon.PlayerInShortArea)
            {
                _dragon.DragonAnimator.SetBool("WalkingBool", false);
                return typeof(MeleeAttackState);
            }
                

            if (!_dragon.PlayerInAreas())
            {
                _dragon.DragonAnimator.SetBool("WalkingBool", false);
                return typeof(IdleState);
            }
             




            return null;
        }

        private void Walk()
        {
            _dragon.DragonAnimator.SetBool("WalkingBool", true);

            if (_dragon.IsRightDirection)
            {
                _dragon.Rb.velocity = Vector2.right * _dragon.speed;
            }
            else
            {
                _dragon.Rb.velocity = Vector2.left * _dragon.speed;
            }

        }

        

    }
}
