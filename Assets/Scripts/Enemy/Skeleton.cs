using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IPooledObject {

    

    public void OnReturnToPool() {
        Debug.Log("Skeleton despawned!");
    }

    public void OnSpawnFromPool() {
        Debug.Log("Skeleton spawned!");
    }
}
