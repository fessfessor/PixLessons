using UnityEngine;
using System.Collections;
using System;
namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{
    public class IdleState : BaseState
    {
        SkeletonDragonBoss _dragon;
        public IdleState(SkeletonDragonBoss _dragon) : base(_dragon.gameObject)
        {
            this._dragon = _dragon;
        }

        public override Type Tick()
        {
            /*
            if (_dragon.PlayerInShortArea)
                return typeof(MeleeAttackState);


            if (_dragon.PlayerInLongArea)
                return typeof(FireAttackState);


            if (_dragon.PlayerInBackArea)
                return typeof(WalkState);
                */

            if (_dragon.PlayerInBackArea || _dragon.PlayerInLongArea || _dragon.PlayerInShortArea)
                return typeof(MeleeAttackState);



            return null;
        }

        


    }
}