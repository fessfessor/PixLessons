using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour {
    [SerializeField]private int healthCount;
    private int maxHealth;

    private Vector3 startPosition;



    private void Start() {
        GameManager.Instance.healthContainer.Add(gameObject, this);
        maxHealth = healthCount;      
    }

    private void OnEnable() {
        startPosition = transform.position;
    }

    public int HealthCount { get => healthCount; set => healthCount = (value < maxHealth) ? value : maxHealth; }
    

    public void takeHit(int damage) {
        HealthCount -= damage;
        if (HealthCount <= 0 && gameObject != GameManager.Instance.player) {
            Destroy(gameObject, 0.5f);
        }
            //     
    }

    IEnumerator CustomDestroy() {
        yield return new WaitForSeconds(0.5f);

    }
   

}
