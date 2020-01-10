using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerLogic {
    public class JumpState : BaseState {

        private Player _player;
        private Rigidbody2D rb;
        private bool isJumping;
        private bool firstEnterInState = true;



        public JumpState(Player _player) : base(_player.gameObject) {
            this._player = _player;
            //rb = _player.Rb;
            rb = transform.GetComponent<Rigidbody2D>();          
        }

        public override Type Tick() {

            CharacterJump();

            if (_player.CharacterPressRunning())
                return typeof(RunState);

            

            return typeof(IdleState); // TODO доделать состояние прыжка
        }

        public void CharacterJump()
        {                      
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * _player.Force, ForceMode2D.Impulse);
            _player.Animator.SetTrigger("startJump");
    
        }



    }
}
