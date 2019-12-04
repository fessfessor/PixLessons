using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    
    
    

   

    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";
        bool isEnemy = col.gameObject.transform.CompareTag("Enemy");

        if (isPlayer) {
            GameManager.Instance.healthContainer[col.gameObject].takeHit(damage);
        }else if (isEnemy) {
            GameManager.Instance.healthContainer[col.gameObject].takeHit(damage);
        }
        

    }
}
