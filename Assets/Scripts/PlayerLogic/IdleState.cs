
using System;
using System.Collections;
using System.Collections.Generic;


namespace Assets.Scripts.PlayerLogic
{

    public class IdleState : BaseState
    {
        private Player _player;


        public IdleState(Player _player) : base(_player.gameObject)
        {
            this._player = _player;
        }

        public override Type Tick()
        {
            throw new NotImplementedException();
        }

        private void CheckAndAttack()
        {

        }

        private void CheckAndJump()
        {

        }

        private void CheckAndRoll()
        {

        }

        private void CheckAndMagicAttack()
        {

        }

        private void CheckAndRun()
        {

        }
    }
}
