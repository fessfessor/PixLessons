using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int cointsCount;
    [SerializeField] private Text flameCoinsText;
    private ObjectPooler pooler;

    private List<Item> items;
    public List<Item> Items { get => items; }

    private void Start() {
        GameManager.Instance.inventory = this;
        flameCoinsText.text = cointsCount + "";
        pooler = ObjectPooler.Instance;
        items = new List<Item>();

        
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if (GameManager.Instance.flameCoinContainer.ContainsKey(col.gameObject)) {
            cointsCount++;
            // Обновляем поле в UI
            flameCoinsText.text = cointsCount + ""; 

            var flameCoin = GameManager.Instance.flameCoinContainer[col.gameObject];
            flameCoin.TakeCoin();
            pooler.ReturnToPool("FlameCoin", col.gameObject, 2f);

        }

        Debug.Log(col.transform.name + " " + GameManager.Instance.itemsContainer.ContainsKey(col.gameObject));
        if (GameManager.Instance.itemsContainer.ContainsKey(col.gameObject)) {
            var itemComponent = GameManager.Instance.itemsContainer[col.gameObject];
            items.Add(itemComponent.Item);
            pooler.ReturnToPool("Potion", col.gameObject, 1f);

            
        }
    }



}
