﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;

    [SerializeField]
    GameObject disPlatformPrefab;

    void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(disPlatformPrefab, new Vector2(-3.5f, -2.5f), disPlatformPrefab.transform.rotation);
        Instantiate(disPlatformPrefab, new Vector2(0f, -2.5f), disPlatformPrefab.transform.rotation);
        Instantiate(disPlatformPrefab, new Vector2(3.5f, -2.5f), disPlatformPrefab.transform.rotation);
    }

    IEnumerator SpawnPlatform (Vector2 sp) {
        yield return new WaitForSeconds(2f);
        Instantiate(disPlatformPrefab, sp, disPlatformPrefab.transform.rotation);

    }
}
