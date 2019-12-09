using System;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonFootman
{
    public class BlockingState : BaseState
    {
        private SkeletonFootman _footman;
        public BlockingState(SkeletonFootman _footman) : base(_footman.gameObject)
        {
            this._footman = _footman;
        }

        public override Type Tick()
        {
            throw new NotImplementedException();
        }
    }
}
