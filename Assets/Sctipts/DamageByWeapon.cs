﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageByWeapon : MonoBehaviour
{
    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] GameObject parent;

    private ObjectPooler pooler;
    private GameObject BloodSplash;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
    }




    private void OnTriggerEnter2D(Collider2D col) {
        
        // Чтобы мы не дамажили своим же оружием себя
        if (parent != null && parent.transform.name != col.transform.name) {           
                if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject)) {
                    var health = GameManager.Instance.healthContainer[col.gameObject];
                    // Если есть здоровье, его больше 0 и 
                    if (health != null && health.HealthCount > 0) {
                        health.takeHit(damage);
                        StartCoroutine(Blood(col.transform.position));
                        

                    }
                }
            
                
        }
    }

    IEnumerator Blood(Vector2 position)
    {
        BloodSplash = pooler.SpawnFromPool("BloodSplash", position, Quaternion.identity);
        yield return new WaitForSeconds(1f);       
        BloodSplash.GetComponent<Animator>().WriteDefaultValues();
        pooler.ReturnToPool("BloodSplash", BloodSplash);
        

    }
        
       
            
 
}

