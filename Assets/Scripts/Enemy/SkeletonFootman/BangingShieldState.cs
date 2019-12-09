using System;

namespace Assets.Scripts.Enemy.SkeletonFootman
{
    public class BangingShieldState : BaseState
    {

        private SkeletonFootman _footman;
        
        public BangingShieldState(SkeletonFootman _footman) : base(_footman.gameObject)
        {
            this._footman = _footman;
        }

        public override Type Tick()
        {


            return null;
        }
        
        private void BangingShield()
        {
            _footman.anim.SetTrigger("BangingShield");
        }
    }
}
