
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerLogic
{

    public class IdleState : BaseState
    {
        private Player _player;
        private bool useComputerMode;
        private Joystick joystick;


        public IdleState(Player _player) : base(_player.gameObject)
        {
            this._player = _player;
            useComputerMode = _player.UseComputerMode;
            joystick = _player.Joystick;
        }

        public override Type Tick()
        {
            if (_player.GroundD.isGrounded)
            {

                if (_player.CharacterPressJumping())
                    return typeof(JumpState);
            }

            if (_player.CharacterPressRunning())
                return typeof(RunState);


            return null;
        }

        


        
    }
}
