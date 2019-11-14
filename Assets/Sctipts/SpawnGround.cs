using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGround : MonoBehaviour, IPooledObject
{
    private WaitForSeconds timer;
    ObjectPooler pooler;
    private Collider2D collider2d;

    private void Start() {
        timer = new WaitForSeconds(0.5f);
        pooler = ObjectPooler.Instance;
        collider2d = GetComponent<Collider2D>();
    }

    public void OnReturnToPool() {
        
    }

    public void OnSpawnFromPool() {
        
    }

    

    private void OnTriggerStay2D(Collider2D col) {
        if(col.gameObject == GameManager.Instance.darkWall) {
            if(Mathf.Abs(GameManager.Instance.darkWall.transform.position.x - collider2d.bounds.max.x) < 1)
                pooler.ReturnToPool(transform.name.Replace("(Clone)", ""), gameObject);
        }
    }

}
