using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.PlayerLogic {
    public class JumpState : BaseState {

        private Player _player;
        private Rigidbody2D rb;
        private bool isJumping;

        public JumpState(Player _player) : base(_player.gameObject) {
            this._player = _player;
            //rb = _player.Rb;
            rb = transform.GetComponent<Rigidbody2D>();
            EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_START_JUMP, OnEvent);
            EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_END_JUMP, OnEvent);
        }

        public override Type Tick() {
            if(!isJumping)
                CharacterJump();

            if (isJumping)
                return typeof(JumpState);
            else if (!isJumping)
                return typeof(IdleState);


            if (_player.CharacterPressRunning())
                return typeof(RunState);


            return null;
        }

        private void CharacterJump()
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector2.up * _player.Force, ForceMode2D.Impulse);
            _player.Animator.SetTrigger("startJump");
        }

        private void CharacterStartJump()
        {
            isJumping = true;
        }

        private void CharacterEndJump()
        {
            isJumping = false;
            AudioManager.Instance.Play("JumpLanding");            
        }

        void OnEvent(EVENT_TYPE eventType, Component sender, object param = null)
        {
            switch (eventType)
            {                              
                case EVENT_TYPE.PLAYER_END_JUMP:
                CharacterEndJump();
                break;
                case EVENT_TYPE.PLAYER_START_JUMP:
                CharacterStartJump();
                break;
                default:
                break;
            }

        }



    }
}
