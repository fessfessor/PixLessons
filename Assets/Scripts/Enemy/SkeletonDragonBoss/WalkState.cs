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
            throw new NotImplementedException();
        }

        

    }
}
