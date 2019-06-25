using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public Dictionary<GameObject, Health> healthContainer;

    private void Awake() {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        
    }

}   
