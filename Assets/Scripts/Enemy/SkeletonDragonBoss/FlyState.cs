using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{
    public class FlyState : BaseState
    {
        SkeletonDragonBoss _dragon;
        public FlyState(SkeletonDragonBoss _dragon) : base(_dragon.gameObject)
        {
            this._dragon = _dragon;
        }

        public override Type Tick()
        {
            Fly();

            if (!_dragon.DragonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fly"))
            {
                return typeof(WalkState);
            }

            return null;
        }


        private void Fly()
        {
            _dragon.DragonAnimator.SetBool("FlyBool", true);
        }


    }
}