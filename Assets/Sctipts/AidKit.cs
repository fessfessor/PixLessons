using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour, IPooledObject {
    [SerializeField] private int healthSize;
    [SerializeField] Animator anim;

    ObjectPooler pooler;


    private void Start() {
        if (!pooler)
            pooler = ObjectPooler.Instance;
    }

    public IEnumerator OnReturnToPool(GameObject gameObject, float delay) {       
        yield return new WaitForSeconds(delay);
        anim.WriteDefaultValues();
        pooler.ReturnToPool("AidKit", gameObject);
    }

    public IEnumerator OnSpawnFromPool(float delay) {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        Health health = col.gameObject.GetComponent<Health>();
        if (health != null) {
            health.HealthCount += healthSize;
            anim.SetTrigger("isConsume");
            // Закидываем объект в пул
            StartCoroutine(OnReturnToPool(gameObject, 1f));
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
