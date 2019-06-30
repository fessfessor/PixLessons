using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTriggerDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";

        if (isPlayer) {
            GameManager.Instance.healthContainer[col.gameObject].takeHit(damage);
        }
    }
}
