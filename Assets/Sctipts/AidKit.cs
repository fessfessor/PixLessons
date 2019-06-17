using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField]
    private int healthSize;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.GetComponent<Health>() != null) {
            col.gameObject.GetComponent<Health>().health += healthSize;
            Destroy(gameObject);
        }
    }



}
