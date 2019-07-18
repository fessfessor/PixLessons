using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int cointsCount;
    [SerializeField] private Text flameCoinsText;

    private void Start() {
        flameCoinsText.text = cointsCount + "";
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if (GameManager.Instance.flameCoinContainer.ContainsKey(col.gameObject)) {
            cointsCount++;
            // Обновляем поле в UI
            flameCoinsText.text = cointsCount + ""; 

            var flameCoin = GameManager.Instance.flameCoinContainer[col.gameObject];
            StartCoroutine(flameCoin.OnReturnToPool(col.gameObject, 0.5f));
            




            
        }
    }



}
