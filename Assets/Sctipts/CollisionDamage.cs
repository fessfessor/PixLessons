using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Range(10,100)]
    public int damage = 10;

    public string colTag;
    

    //Сюда надо передать тег КОГО дамажим при касании
    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag(colTag)) {
            Health health = col.gameObject.GetComponent<Health>();
            health.takeHit(damage);           
            //Показать хелс бар
            // Берем все чайлды, находим хелс бар и показываем его
            for (int i = 0; i < col.gameObject.transform.childCount; i++) {
                if (col.gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                    col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                }
            }

        }
    }


    
    
}
