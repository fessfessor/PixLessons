using UnityEngine;
using System.Collections;
using System;

namespace Assets.Scripts.Enemy.SkeletonDragonBoss
{
    public class DeathState : BaseState
    {
        SkeletonDragonBoss _dragon;
        public DeathState(SkeletonDragonBoss _dragon) : base(_dragon.gameObject)
        {
            this._dragon = _dragon;
        }

        public override Type Tick()
        {
            throw new NotImplementedException();
        }


    }
}