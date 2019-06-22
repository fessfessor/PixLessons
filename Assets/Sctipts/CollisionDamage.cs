using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Range(10,100)]
    public int damage = 10;

    public string colTag;
    [SerializeField] private Animator animator;
    private Health health;
    

    //Сюда надо передать тег КОГО дамажим при касании
    private void OnCollisionStay2D(Collision2D col) {      
        health = col.gameObject.GetComponent<Health>();

            // Берем все чайлды, находим хелс бар и показываем его
            for (int i = 0; i < col.gameObject.transform.childCount; i++) {
                if (col.gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                    col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                }
            }

       
    }

    private void OnCollisionEnter2D(Collision2D col) {
        health = col.gameObject.GetComponent<Health>();
        if (health != null) {
            health.takeHit(damage);
        }
        // Берем все чайлды, находим хелс бар и показываем его
        for (int i = 0; i < col.gameObject.transform.childCount; i++) {
            if (col.gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    public void SetDamage() {   

        if (health != null) {
            health.takeHit(damage);
        }
        health = null;
    }


    
    
}
