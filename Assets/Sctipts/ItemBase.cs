using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Items database", menuName = "Databases")]
public class ItemBase : ScriptableObject
{
    [SerializeField] private List<Item> items;
    [SerializeField] private Item currentItem;

    private int currentIndex;

    public void CreateItem() {
        if (items == null)
            items = new List<Item>();

        Item item = new Item();
        items.Add(item);
        currentItem = item;
        currentIndex = items.Count - 1;
    }

    public void RemoveItem() {
        if (items == null || currentItem == null)
            return;

        items.Remove(currentItem);

        if (items.Count > 0)
            currentItem = items[0];
        else
            CreateItem();
        currentIndex = 0;
        
    }

    public void NextItem() {
        if(currentIndex + 1 < items.Count) {
            currentIndex++;
            currentItem = items[currentIndex];
        }
    }

    public void PrevItem() {
        if (currentIndex > 0) {
            currentIndex--;
            currentItem = items[currentIndex];
        }
    }
}



public class Item {
    [SerializeField] private int id;
    public int ID { get => id; }

    [SerializeField] private string itemName;
    public string ItemName { get => itemName; }

    [SerializeField] private string description;
    public string Description { get => description; }

    [SerializeField] private BuffType buffType;
    public BuffType BuffType { get => buffType; }

    [SerializeField] private float value;
    public float Value { get => value; }

    public override string ToString() {
        return "Id - " + id + " .Description - " + description + " .Buff type - " + buffType + " .Value - " + value;
    }

}
