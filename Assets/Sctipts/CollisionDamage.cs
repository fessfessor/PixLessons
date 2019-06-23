using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Range(10,100)]
    [SerializeField] private int damage = 10;


    
    private Health health;

    

    //Сюда надо передать тег КОГО дамажим при касании
   

    private void OnCollisionEnter2D(Collision2D col) {
        //Debug.Log("CollisionEnter");
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

    private void OnTriggerStay2D(Collider2D col) {
        

        health = col.gameObject.GetComponent<Health>();
        col.gameObject.GetComponent<Rigidbody2D>().WakeUp();
        
        // Берем все чайлды, находим хелс бар и показываем его
        for (int i = 0; i < col.gameObject.transform.childCount; i++) {
            if (col.gameObject.transform.GetChild(i).transform.name == "HealthBar") {
                col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    

    public void SetDamage() {
       // Debug.Log("HEALTH NULL " + (health == null));
        //Debug.Log("SET DAMAGE " + health.health);
        
        if (health != null) {
            health.takeHit(damage);
        }
        health = null;
    }


    
    
}
