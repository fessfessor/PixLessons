using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField] ENEMY_TYPE enemyType;

    private SpawnGround parent;
    private ObjectPooler pooler;

    void Start() {
        EventManager.Instance.AddListener(EVENT_TYPE.SPAWN_ENEMY, OnEvent);
        parent = GetComponentInParent<SpawnGround>();
        pooler = ObjectPooler.Instance;
    }

    

    void OnEvent(EVENT_TYPE eventType, Component sender, object param = null) {
        if(eventType == EVENT_TYPE.SPAWN_ENEMY) {
            if(sender == parent) { // Если отправитель это наш кусок земли, то спавним врагов
                if (SpawnManager.RandomPercent(100)) {
                    SpawnEnemy();
                }
            }
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);

    }

    void SpawnEnemy() {
        Debug.Log("SKELETON!");

        pooler.SpawnFromPool(enemyType.ToString(), transform.position, Quaternion.identity);
    }
   
}
