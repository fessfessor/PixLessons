using System;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonArcher
{
    public class ShootingState : BaseState {
        private SkeletonArcher _archer;
        private float timer;
        private bool firstEnterInState;

        public ShootingState(SkeletonArcher _archer) : base(_archer.gameObject)
        {
            this._archer = _archer;
            timer = _archer.ShootFreq;
        
        }

        public override Type Tick()
        {
            //Система позволяющая узнать только что мы перешли в это состояние или же мы уже в нем долго
            CheckTransitionState();

            if (timer > _archer.ShootFreq)
            {
                Shooting();
                timer = 0;
            }
            

            if (!_archer.inShootArea)
            {
                if (_archer.type == SkeletonArcher.ArcherType.PATROL)
                    return typeof(PatrolState);

                return typeof(WatchingState);
            }

            if (_archer.inAttackArea)
            {
                return typeof(HittingState);
            }


            firstEnterInState = false;
            return null;
        }

        private void Shooting() {
            if(_archer.enemy.transform.position.x < transform.position.x && _archer.isRightDirection
               ||
               _archer.enemy.transform.position.x > transform.position.x && !_archer.isRightDirection
            )
                _archer.SwitchVision();


            _archer.anim.SetTrigger("Shoot");
            _archer.rb.velocity = Vector2.zero;
   
        }

        private void CheckTransitionState()
        {
            if (!firstEnterInState) {
                firstEnterInState = true;
                timer += Time.deltaTime;
            }
            else {
                timer = _archer.ShootFreq;
            }
        }
    }
}
