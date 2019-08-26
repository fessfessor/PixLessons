using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] Cell[] cells;

    PlayerInventory inventory;

    void Start()
    {
        var inventory = GameManager.Instance.inventory;
        for (int i=0; i < inventory.Items.Count; i++) {
            if (i < cells.Length)
                cells[i].Init(inventory.Items[i]);

        }
    }

    void Update()
    {
        
    }
}
