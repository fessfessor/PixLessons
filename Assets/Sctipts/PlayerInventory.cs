using UnityEngine.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int cointsCount;
    [SerializeField] private Text flameCoinsText;
    private ObjectPooler pooler;

    private void Start() {
        flameCoinsText.text = cointsCount + "";
        pooler = ObjectPooler.Instance;
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if (GameManager.Instance.flameCoinContainer.ContainsKey(col.gameObject)) {
            cointsCount++;
            // Обновляем поле в UI
            flameCoinsText.text = cointsCount + ""; 

            var flameCoin = GameManager.Instance.flameCoinContainer[col.gameObject];
            flameCoin.TakeCoin();
            pooler.ReturnToPool("FlameCoin", col.gameObject, 3f);



        }
    }



}
