using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField] SPAWN SPAWN;

    private GameObject parent;
    private ObjectPooler pooler;

    void Start() {
        EventManager.Instance.AddListener(EVENT_TYPE.SPAWN_GROUND, OnEvent);
        parent = transform.parent.gameObject;
        pooler = ObjectPooler.Instance;

    }



    

    void OnEvent(EVENT_TYPE eventType, Component sender, object param = null) {
        if(eventType == EVENT_TYPE.SPAWN_GROUND) {
            if(sender.transform.gameObject == parent) { // Если отправитель это наш кусок земли, то спавним врагов              
                    Spawn();               
            }
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);

    }

    void Spawn() {
        Debug.Log("SKELETON!");

        pooler.SpawnFromPool(SPAWN.ToString(), transform.position, Quaternion.identity);
    }

    
   
}
