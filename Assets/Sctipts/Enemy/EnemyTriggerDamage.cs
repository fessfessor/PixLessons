using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool CanReflectBuSword;
    private GameObject parent;
    private GhostMove parentScript;

    private void Start() {
        if (transform.parent.gameObject) {
            parent = transform.parent.gameObject;
            parentScript = GameManager.Instance.ghostContainer[parent];
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";

        if (isPlayer) {
            GameManager.Instance.healthContainer[col.gameObject].takeHit(damage);
        }else if (col.gameObject.CompareTag("HeroSword") && CanReflectBuSword) { // Если шлепаем мечом , например по прзрачному шару, то отражаем его во владельца
            parentScript.KillYouSelf();
        }


    }
}
