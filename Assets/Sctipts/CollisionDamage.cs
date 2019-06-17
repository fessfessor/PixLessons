using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [Range(10,100)]
    public int damage = 10;

    public string colTag;

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag(colTag)) {
            Health health=  col.gameObject.GetComponent<Health>();
            health.takeHit(damage);


        }
        
    }
}
