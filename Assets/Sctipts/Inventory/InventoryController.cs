using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    #region variables
    [SerializeField] Cell[] cells;
    [SerializeField] private int cellCount;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private Transform rootParent;


    PlayerInventory inventory;
    #endregion

    private void Start() {
        if (cells == null || cells.Length <= 0)
            Init();
    }

    private void OnEnable() {
        if (cells == null || cells.Length <= 0)
            Init();

        var inventory = GameManager.Instance.inventory;
        for (int i = 0; i < inventory.Items.Count; i++) {
            if (i < cells.Length)
                cells[i].Init(inventory.Items[i]);

        }
    }

    void Init()
    {
        cells = new Cell[cellCount];
        for(int i = 0; i < cellCount; i++) {
            cells[i] = Instantiate(cellPrefab, rootParent);

        }
        cellPrefab.gameObject.SetActive(false);


    }


   
}
