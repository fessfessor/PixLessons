using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour, IPooledObject
{
    Rigidbody2D rb;
    [SerializeField]private Collider2D collider;
    [SerializeField]private float fallingDelay;

    private ObjectPooler pooler;

    void Start(){
        // Инициализируем компонент и пул
        rb = GetComponent<Rigidbody2D>();
        pooler = ObjectPooler.Instance;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            //Если на платформу прыгнул игрок, то запускаем спавн платформы на том же месте, а старую дропаем в пул
            StartCoroutine(OnSpawnFromPool(fallingDelay +  3f));
            StartCoroutine(OnReturnToPool(gameObject,fallingDelay + 2f));
        }
       
    }

   // private void OnCollisionEnter2D(Collision2D col) {
    //    if (!col.gameObject.CompareTag("Player")) {
   //         StartCoroutine(OnReturnToPool(gameObject, 0f));
   //     }
   // }





    // Действия при возврате платформы
    public IEnumerator OnReturnToPool(GameObject gameObject, float delay) {
        
        yield return new WaitForSeconds(fallingDelay);
        rb.isKinematic = false;
        collider.enabled = false;       
        yield return new WaitForSeconds(delay);
        rb.isKinematic = true;
        collider.enabled = true;
        pooler.ReturnToPool("FallingPlatform", this.gameObject);
        
    }

    public IEnumerator OnSpawnFromPool(float delay) {
        //Сохраняем начальную позицию
        Vector3 temp = transform.position;
        yield return new WaitForSeconds(delay);
        pooler.SpawnFromPool("FallingPlatform", temp, Quaternion.identity);
    }
}
