using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int cointsCount;
    


    private void OnTriggerEnter2D(Collider2D col) {
        if (GameManager.Instance.flameCoinContainer.ContainsKey(col.gameObject)) {
            cointsCount++;
            var flameCoin = GameManager.Instance.flameCoinContainer[col.gameObject];
            flameCoin.StartDestroy();
            
        }
    }



}
