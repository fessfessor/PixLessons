using System;
using UnityEngine;

namespace Assets.Scripts.Enemy.SkeletonArcher
{
    public class HittingState : BaseState {
        private SkeletonArcher _archer;
        private float timer;
        private bool firstEnterInState;

        public HittingState(SkeletonArcher _archer) : base(_archer.gameObject)
        {
            this._archer = _archer;
            timer = _archer.attackFreq;
        }

        public override Type Tick()
        {

            CheckTransitionState();

            if (timer > _archer.attackFreq)
            {
                Hitting();
                timer = 0;
            }

            if (!_archer.inAttackArea)
            {
                if (_archer.type == SkeletonArcher.ArcherType.PATROL)
                    return typeof(PatrolState);

                return typeof(WatchingState);
            }

            firstEnterInState = true;
            return null;

        }

        private void Hitting()
        {
            _archer.rb.velocity = Vector2.zero;
            _archer.anim.SetTrigger("Hit");
        }
        private void CheckTransitionState() {
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
