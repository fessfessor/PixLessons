using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunIcon : MonoBehaviour, IPooledObject
{
    private ObjectPooler pooler;
    private void Start() {
        pooler = ObjectPooler.Instance;
    }
    public IEnumerator OnReturnToPool(GameObject gameObject, float delay) {

        // Ждем необходимое время для анимации
        yield return new WaitForSeconds(delay);       
        pooler.ReturnToPool("StunIcon", gameObject);
    }

    public IEnumerator OnSpawnFromPool(float delay) {
        throw new System.NotImplementedException();
    }

   
}
