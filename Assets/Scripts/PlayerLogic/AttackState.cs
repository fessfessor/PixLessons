using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.PlayerLogic {
    public class AttackState : BaseState {
        private Player _player;

        public AttackState(Player _player) : base(_player.gameObject) {
            this._player = _player;
        }

        public override Type Tick() {
            throw new NotImplementedException();
        }
    }
}
