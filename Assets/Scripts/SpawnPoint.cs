using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    [SerializeField] SPAWN SPAWN;

    private GameObject parent;
    private ObjectPooler pooler;

   

    private void OnEnable() {
        if(parent ==null)
            parent = transform.parent.gameObject;

        if(pooler==null)
            pooler = ObjectPooler.Instance;

        if(EventManager.Instance != null)
            EventManager.Instance.AddListener(EVENT_TYPE.SPAWN_GROUND, OnEvent);

    }





    void OnEvent(EVENT_TYPE eventType, Component sender, object param = null) {      
        if(eventType == EVENT_TYPE.SPAWN_GROUND) {
            if((Object)param == parent) { // Если отправитель это наш кусок земли, то спавним            
                    Spawn();               
            }
        }

    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 0.15f);

    }



    // Главный метод спавна, который в зависимости от того что выбрано в точке спавна, спавнит это при появлении блока земли
    void Spawn() {

        pooler.SpawnFromPool(SPAWN.ToString(), transform.position, Quaternion.identity);
    }

    
   
}
