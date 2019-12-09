using System;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonFootman
{
    public class WatchingState : BaseState
    {
        private SkeletonFootman _footman;
        private Rigidbody2D rb;
        private float lookingTimer = 0f;
        public WatchingState(SkeletonFootman _footman) : base(_footman.gameObject)
        {
            this._footman = _footman;
            rb = transform.GetComponent<Rigidbody2D>();
        }

        public override Type Tick()
        {
            
            
            lookingTimer += Time.deltaTime;
            if (lookingTimer > 5f)
            {
                Looking();
                lookingTimer = 0;
            }
            else if(!_footman.anim.GetCurrentAnimatorStateInfo(0).IsName("Looking"))
            {
                Moving();    
            }
                
            
            

            if (_footman.inAgrArea)
            {
                _footman.anim.SetBool("isWatching", false);
                return typeof(ChasingState);
            }
            if (_footman.inAttackArea)
            {
                _footman.anim.SetBool("isWatching", false);
                return typeof(AttackingState);
            }

            return null;
        }
        
        
        private void Moving()
        {
            _footman.anim.SetBool("isWatching", true);

            if (_footman.isRightDirection && !_footman.isDeath) {
                rb.velocity = Vector2.right * _footman.speed;
                if (transform.position.x > _footman.rightBorderPosition.x) {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    _footman.isRightDirection = false;
                }
            }
            else if (!_footman.isRightDirection && !_footman.isDeath) {
                rb.velocity = Vector2.left * _footman.speed;

                if (transform.position.x < _footman.leftBorderPosition.x) {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    _footman.isRightDirection = true;
                }
            }
            else if (_footman.isDeath)
                rb.velocity = Vector3.zero;
        }

        private void Looking()
        {
            rb.velocity = Vector2.zero;
            
            _footman.anim.SetTrigger("Looking");
        }
    }
}
