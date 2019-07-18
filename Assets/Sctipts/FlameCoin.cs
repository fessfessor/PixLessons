using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCoin : MonoBehaviour, IPooledObject
{
    [SerializeField] private Animator animator;
    private ObjectPooler pooler;

    private void Start() {
        // Добавляем себя в гейм менеджер
        GameManager.Instance.flameCoinContainer.Add(gameObject,this);

        //Получаем инстанс пулера
        if (!pooler)
            pooler = ObjectPooler.Instance;
    }

    public IEnumerator OnSpawnFromPool(float delay) {
        throw new System.NotImplementedException();
    }

    public IEnumerator OnReturnToPool(GameObject gameObject, float delay) {
        animator.SetTrigger("TakeFlame");
        yield return new WaitForSeconds(delay);
        animator.WriteDefaultValues();
        pooler.ReturnToPool("FlameCoin", gameObject);
    }
}
