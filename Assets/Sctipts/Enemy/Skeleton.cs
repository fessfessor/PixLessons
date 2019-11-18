using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IPooledObject {

    private void Start() {
        
    }


    public void OnReturnToPool() {
        Debug.Log("Skeleton spawned!");
    }

    public void OnSpawnFromPool() {
        Debug.Log("Skeleton despawned!");
    }
}
