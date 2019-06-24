using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByWeapon : MonoBehaviour
{
    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] GameObject parent;

    

    
    private void OnTriggerEnter2D(Collider2D col) {

        if (parent != null) {
            if (col.gameObject.transform.name != parent.transform.name) {
                Health health = col.gameObject.GetComponent<Health>();
                // Если есть здоровье, его больше 0 и 
                if (health != null && health.HealthCount > 0) {
                    health.takeHit(damage);


                    // Берем все чайлды, находим хелс бар и показываем его
                    if (health.HealthCount > 0)
                        PlatformerTools.ShowHealthBar(col.gameObject);
                }
            }
        }
        
       
            
 
    }
}
