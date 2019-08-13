using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour, IPooledObject {
    [SerializeField] private int healthSize;
    private Animator animator;

    ObjectPooler pooler;


    private void Start() {
        if (!pooler)
            pooler = ObjectPooler.Instance;

        GameManager.Instance.pooledObjectContainer.Add(gameObject, this);
        animator = GetComponent<Animator>();
    }

    public void OnSpawnFromPool() {
        
    }

    public void OnReturnToPool() {
        animator.WriteDefaultValues();
    }

   

    private void OnTriggerEnter2D(Collider2D col) {
        Health health = col.gameObject.GetComponent<Health>();
        if (health != null) {
            health.HealthCount += healthSize;
            animator.SetTrigger("isConsume");
            // Закидываем объект в пул
            pooler.ReturnToPool("AidKit", gameObject, 0.5f);
        }
        
        //Показать хелс бар
        // Берем все чайлды, находим хелс бар и показываем его
        for (int i = 0; i < col.gameObject.transform.childCount; i++) {
            if (col.gameObject.transform.GetChild(i).transform.name == "HealthBar") {               
                col.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;                
            }
           
        }
    }

    
}
