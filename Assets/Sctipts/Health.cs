using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour {
    [SerializeField]private int healthCount;



    private void Start() {
        GameManager.Instance.healthContainer.Add(gameObject, this);
    }

    public int HealthCount { get => healthCount; set => healthCount = value; }
    

    public void takeHit(int damage) {
        HealthCount -= damage;
        if (HealthCount <= 0)
            Destroy(gameObject, 0.5f);      
    }

   

}
