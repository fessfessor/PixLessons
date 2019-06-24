using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Range(10,100)]
    [SerializeField] private int damage = 10;

    private Health health;

    


    private void OnCollisionEnter2D(Collision2D col) {
        //Debug.Log("CollisionEnter");
        health = col.gameObject.GetComponent<Health>();
        if (health != null) {
            health.takeHit(damage);

            //Если есть еще хп, показываем хелсбар
            if(health.HealthCount > 0)
                PlatformerTools.ShowHealthBar(col.gameObject);
        }
        
        
        
    }

    private void OnTriggerStay2D(Collider2D col) {
        health = col.gameObject.GetComponent<Health>();
        //col.gameObject.GetComponent<Rigidbody2D>().WakeUp(); 
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
