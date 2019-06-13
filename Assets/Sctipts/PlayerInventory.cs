using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int cointsCount;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Coin")) {
            cointsCount++;
            Debug.Log("coints = " + cointsCount);
            Destroy(col.gameObject);
        }
    }
}
