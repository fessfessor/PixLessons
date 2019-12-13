using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemy.SkeletonFootman
{
    public class AttackingState : BaseState
    {
        private SkeletonFootman _footman;
        private float timer;
        private bool firstEnterInState;
        public AttackingState(SkeletonFootman _footman) : base(_footman.gameObject)
        {
            this._footman = _footman;
            timer = _footman.attackFreq;
        }

        public override Type Tick()
        {
            CheckTransitionState();
            timer += Time.deltaTime;

            if (!_footman.inAttackArea && !InAttackAnimation())
                return typeof(ChasingState);

            if (!_footman.inAgrArea && !InAttackAnimation())
                return typeof(WatchingState);

            if(timer>_footman.attackFreq)
                Attack();

            firstEnterInState = false;
            return null;
        }

        private void Attack()
        {
            int randomClip;

            if (!InAttackAnimation()) {

                randomClip = Random.Range(1, 5);
                switch (randomClip) {
                    case 1:
                        _footman.anim.Play("Thrust");
                        break;
                    case 2:
                        _footman.anim.Play("Double_thrust");
                        break;
                    case 3:
                        _footman.anim.Play("Hit_down");
                        break;
                    case 4:
                        _footman.anim.Play("Shield");
                        break;
                    default:
                        _footman.anim.Play("Thrust");
                        break;
                }

                timer = 0;

            }
        }

        private bool InAttackAnimation()
        {
            return  _footman.anim.GetCurrentAnimatorStateInfo(0).IsName("Thrust")
                ||
                _footman.anim.GetCurrentAnimatorStateInfo(0).IsName("Double_thrust")
                ||
                _footman.anim.GetCurrentAnimatorStateInfo(0).IsName("Hit_down")
                ||
                _footman.anim.GetCurrentAnimatorStateInfo(0).IsName("Shield");
        }


        private void CheckTransitionState() {
            if (!firstEnterInState) {
                firstEnterInState = true;
                timer += Time.deltaTime;
            }
            else {
                timer = _footman.attackFreq;
            }
        }



    }
}
