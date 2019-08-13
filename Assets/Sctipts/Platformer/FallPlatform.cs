using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour, IPooledObject
{
    private Rigidbody2D rb;
    private Vector3 tempPosition;
    private Collider2D coll;
    [SerializeField]private float fallingDelay;
    [SerializeField]private float spawnDelay;

    private ObjectPooler pooler;

    void Start(){
        // Инициализируем компонент и пул
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        GameManager.Instance.pooledObjectContainer.Add(gameObject, this);      
        pooler = ObjectPooler.Instance;
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject == GameManager.Instance.player) {
            //Если на платформу прыгнул игрок, то запускаем спавн платформы на том же месте, а старую дропаем в пул
            StartCoroutine(Falling());            
        }
       
    }


    // Падение платформы, тут же вызывается спавн
    public IEnumerator Falling() {
        tempPosition = transform.position;

        yield return new WaitForSeconds(fallingDelay);
        rb.isKinematic = false;
        coll.enabled = false;
        StartCoroutine(Spawn());
        yield return new WaitForSeconds(2f);
        pooler.ReturnToPool("FallingPlatform", gameObject);
    }

    IEnumerator Spawn() {
        yield return new WaitForSeconds(spawnDelay);
        pooler.SpawnFromPool("FallingPlatform", tempPosition, Quaternion.identity);
    }


    public void OnSpawnFromPool() {
        
    }

    public void OnReturnToPool() {
        rb.isKinematic = true;
        coll.enabled = true;
    }
}
