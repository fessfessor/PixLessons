using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField]
    private int healthSize;

    private void OnTriggerEnter2D(Collider2D col) {
        Health health = col.gameObject.GetComponent<Health>();
        if (health != null) {
            health.health += healthSize;
            Destroy(gameObject);
        }
    }



}
