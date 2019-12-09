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
                return typeof(AttackingState);

            return Moving(); 
            
        }
        
        private Type Moving()
        {
            _footman.anim.SetBool("Run", true);

            if (_footman.isRightDirection && !_footman.isDeath) {
                
                if (transform.position.x > _footman.rightBorderPosition.x)
                {
                    return typeof(BangingShieldState);
                }
                else
                {
                    _footman.rb.velocity = Time.deltaTime * _footman.runSpeed * Vector2.right;
                    return null;
                }
            }
            else if (!_footman.isRightDirection && !_footman.isDeath) {
                

                if (transform.position.x < _footman.leftBorderPosition.x) {
                    return typeof(BangingShieldState);
                }
                else
                {
                    _footman.rb.velocity = Time.deltaTime * _footman.runSpeed * Vector2.left;
                    return null;
                }
            }
            else if (_footman.isDeath)
                _footman.rb.velocity = Vector3.zero;

            return null;
        }

       
    }
}
