using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.PlayerLogic {
    public class DeathState : BaseState {

        private Player _player;
        private bool isDeath = false;

        public DeathState(Player _player) : base(_player.gameObject) {
            this._player = _player;
        }

        public override Type Tick() {
            if (!isDeath)
                CharacterDeath();

            return null;
        }

        private void CharacterDeath()
        {
            _player.Health.HealthCount = 0;                     
            isDeath = true;
            GameManager.Instance.deathMenuUI.SetActive(true);
            AudioManager.Instance.Play("PlayerDie");
            _player.Animator.SetTrigger("death");
        }


    }
}

