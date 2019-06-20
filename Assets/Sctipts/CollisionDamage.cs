using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Range(10,100)]
    public int damage = 10;

    public string colTag;
    //public string colTriggerTag;

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag(colTag)) {
            Health health = col.gameObject.GetComponent<Health>();
            health.takeHit(damage);
            Debug.Log("Hit by " + col.gameObject.transform.name);
            //Показать хелс бар
            // Берем все чайлды, находим хелс бар и показываем его
            for (int i = 0; i < col.gameObject.transform.childCount; i++) {
                if (col.gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                    col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                }
            }

        }
    }
    
    /*
    //TODO доделать получение урока скелетом
    private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.CompareTag(colTag)) {
                Health health = col.gameObject.GetComponent<Health>();
                health.takeHit(damage);
            Debug.Log("Triggered by " + col.gameObject.transform.name);
            //Показать хелс бар
            // Берем все чайлды, находим хелс бар и показываем его
            for (int i = 0; i < col.gameObject.transform.childCount; i++) {
                    if (col.gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                        col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                    }
                }

            }
    }
    */
}
