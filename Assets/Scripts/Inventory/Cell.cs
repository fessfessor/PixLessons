using UnityEngine.UI;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private Image icon;
    private Item item;

    private void Awake() {
        icon.sprite = null;
    }

    public void Init(Item item) {
        this.item = item;
        icon.sprite = item.Icon;
    }
}
