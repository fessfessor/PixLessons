using System;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonFootman
{
    public class ChasingState : BaseState
    {
        private SkeletonFootman _footman;
        
        
        public ChasingState(SkeletonFootman _footman) : base(_footman.gameObject)
        {
            this._footman = _footman;
        }

        public override Type Tick()
        {
            

            if (_footman.inAttackArea)
            {
                _footman.anim.SetBool("isRunning", false);
                return typeof(AttackingState);
            }


            if (!_footman.inAgrArea)
            {
                _footman.anim.SetBool("isRunning", false);
                
                return typeof(WatchingState);
            }

            
           
            Moving();

            return null; 
            
        }
        
        private void Moving()
        {
            if (_footman.isDeath)
                _footman.rb.velocity = Vector3.zero;

            CheckDirection();
            
            _footman.anim.SetBool("isRunning", true);
            

            if (_footman.isRightDirection && !_footman.isDeath) {
                
                if (transform.position.x > _footman.rightBorderPosition.x)
                {
                    BangingShield(); 
                }
                else
                {
                    _footman.rb.velocity = _footman.runSpeed * Vector2.right;
                }
            }
            else if (!_footman.isRightDirection && !_footman.isDeath) 
            {
                

                if (transform.position.x < _footman.leftBorderPosition.x) 
                {
                    BangingShield();
                }
                else
                {
                    _footman.rb.velocity =_footman.runSpeed * Vector2.left;
                }
            }
            

            
        }

        private void BangingShield() {
            _footman.rb.velocity = Vector2.zero;

            if(!_footman.anim.GetCurrentAnimatorStateInfo(0).IsName("Banging_shield"))
                _footman.anim.SetTrigger("BangingShield");
            
        }

        private void CheckDirection()
        {
            //Если враг слева, а смотрим вправо - разворачиваемся и наоборот
            if (transform.position.x > _footman.enemy.transform.position.x
                && _footman.isRightDirection)
            {
                _footman.SwitchVision();
            }

            if (transform.position.x < _footman.enemy.transform.position.x
                && !_footman.isRightDirection) {
                _footman.SwitchVision();
            }
        }

        

    }
}
