using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour, IPooledObject
{
    private Rigidbody2D rb;
    private Vector3 tempPosition;
    private Collider2D coll;
    private SpriteRenderer sr;
    [SerializeField]private float fallingDelay;
    [SerializeField]private float spawnDelay;
    [SerializeField]private bool isFalling = true;

    private ObjectPooler pooler;

    void Start(){
        // Инициализируем компонент и пул
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        pooler = ObjectPooler.Instance;
        sr = GetComponent<SpriteRenderer>();

        if (!isFalling)
            sr.color = Color.white;
        else
            sr.color = new Color(160, 50, 50);

        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject == GameManager.Instance.player) {
            //Если на платформу прыгнул игрок, то запускаем спавн платформы на том же месте, а старую дропаем в пул
            if(isFalling)
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
