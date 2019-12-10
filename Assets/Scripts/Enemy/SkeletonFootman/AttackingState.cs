using System;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonFootman
{
    public class AttackingState : BaseState
    {
        private SkeletonFootman _footman;
        public AttackingState(SkeletonFootman _footman) : base(_footman.gameObject)
        {
            this._footman = _footman;
        }

        public override Type Tick()
        {
            

            return null;
        }

        private void Attack()
        {

        }
    }
}
