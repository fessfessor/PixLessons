using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour {
    [SerializeField]private int healthCount;
    

    private int maxHealth;

    private bool isPooledObj;




    private void Start() {
        GameManager.Instance.healthContainer.Add(gameObject, this);
        
        maxHealth = healthCount;

        isPooledObj = GetComponent<IPooledObject>() != null;
    }

   
    public int HealthCount { get => healthCount; set => healthCount = (value < maxHealth) ? value : maxHealth; }


    public void takeHit(int damage) {
        HealthCount -= damage;
        if (HealthCount <= 0 && gameObject != GameManager.Instance.player) {
            if (isPooledObj) {// Если это объект из пула ,возвращаем его туда
                //TODO Что-нибудь сделать с анимациями смерти, чтобы не хардкодить задержку отправки в пулер
                ObjectPooler.Instance.ReturnToPool(transform.name, gameObject,3f);
            }
            else 
            {
                Destroy(gameObject, 1f);
            }
            
        }
              
    }

   

}
