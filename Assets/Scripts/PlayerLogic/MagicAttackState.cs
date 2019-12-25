using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.PlayerLogic {
    public class MagicAttackState : BaseState {
        private Player _player;

        public MagicAttackState(Player _player) : base(_player.gameObject) {
            this._player = _player;
        }

        public override Type Tick() {
            throw new NotImplementedException();
        }
    }
}

