using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Range(10,100)]
    [SerializeField] private int damage = 10;

    private Health health;
    private GameObject collisionObject;

    


    private void OnCollisionEnter2D(Collision2D col) {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject)) {
            health = GameManager.Instance.healthContainer[col.gameObject];

            health.takeHit(damage);

            //Если есть еще хп, показываем хелсбар
            if (health.HealthCount > 0)
                PlatformerTools.ShowHealthBar(col.gameObject);
        }
        
        
    }

    // этот метод нужен для случая ,когда персонаж просто стоит и его , например, грызет пес
    private void OnTriggerStay2D(Collider2D col) {
        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject)) {
            health = GameManager.Instance.healthContainer[col.gameObject];
            collisionObject = col.gameObject;
            col.gameObject.GetComponent<Rigidbody2D>().WakeUp();

        }
        
    }

    

    public void SetDamage() {

        if (health != null) {
            health.takeHit(damage);
           
            if (health.HealthCount > 0 && collisionObject) 
                PlatformerTools.ShowHealthBar(collisionObject);

        }
        collisionObject = null;
        health = null;
    }


    
    
}
