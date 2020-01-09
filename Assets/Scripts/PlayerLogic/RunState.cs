using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.PlayerLogic
{
    public class RunState : BaseState
    {
        private Player _player;
        private Joystick joystick;
        private Vector3 direction;
        private Rigidbody2D rb;
        

        public RunState(Player _player) : base(_player.gameObject)
        {
            this._player = _player;
            joystick = _player.Joystick;
            //rb = _player.Rb; //TODO непонятно почему не видит этой ссылки
            rb = transform.GetComponent<Rigidbody2D>();


        }

        public override Type Tick()
        {
            CharacterRun();
            SetSpeedInAnimator();
            SwitchCharacterDirection();

            if (!_player.CharacterPressRunning())
                return typeof(IdleState);
            if(_player.CharacterPressJumping())
                return typeof(JumpState);


            return null;
           
        }


        private void CharacterRun()
        {
            direction = Vector3.zero;

            if (joystick.Horizontal >= .2f)
            {
                direction = Vector3.right * _player.Speed;
                direction.y = rb.velocity.y;
                rb.velocity = direction;              
            }
            else if (joystick.Horizontal <= -.2f)
            {
                direction = Vector3.left * _player.Speed;
                direction.y = rb.velocity.y;
                rb.velocity = direction;               
            }
            else
            {
                direction = Vector3.zero;
                direction.y = rb.velocity.y;
                rb.velocity = direction;              
            }


#if UNITY_EDITOR
            if (_player.UseComputerMode)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    direction = Vector3.left;
                    
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    direction = Vector3.right;
                    
                }
               
                direction *= _player.Speed;
                direction.y = rb.velocity.y;
                rb.velocity = direction;
            }


#endif
            

        }

        private void SetSpeedInAnimator()
        {
            _player.Animator.SetFloat("speed", Mathf.Abs(direction.x));
        }

        private void SwitchCharacterDirection()
        {
            if (direction.x > 0)
            {
                _player.IsRightDirection = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (direction.x < 0)
            {
                _player.IsRightDirection = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }
        }
    }

   
}
