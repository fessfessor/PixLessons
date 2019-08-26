using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour, IPooledObject
{

    [SerializeField] private ItemType itemType;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Item item;
    public Item Item { get => item; }

    public void OnReturnToPool() {
        Debug.Log("Зелье вернулось!");
    }

    public void OnSpawnFromPool() {
        Debug.Log("Зелье заспавнилось!");
    }

    void Start()
    {
        item = GameManager.Instance.itemBase.GetItemOfID((int)itemType);
        spriteRenderer.sprite = item.Icon;
        GameManager.Instance.itemsContainer.Add(gameObject, this);
    }


    
}

public enum ItemType {
    MeleePotion, ArmorPotion , HealthPotion , RangePotion
}
