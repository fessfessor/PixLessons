using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;

    [SerializeField]
    GameObject disPlatformPrefab;

    [SerializeField] GameObject parent;

    
    void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);      
    }

    //Спавн платформы после того как она упала
    IEnumerator SpawnPlatform (Vector2 sp) {
        yield return new WaitForSeconds(2f);
        Instantiate(disPlatformPrefab, sp, disPlatformPrefab.transform.rotation, parent.transform);
    }
}
