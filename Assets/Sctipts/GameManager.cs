using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, FlameCoin> flameCoinContainer;
    public Dictionary<GameObject, GhostMove> ghostContainer;

    private void Awake() {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        flameCoinContainer = new Dictionary<GameObject, FlameCoin>();
        ghostContainer = new Dictionary<GameObject, GhostMove>();


    }

    // Кнопка паузы
    public void OnClickPause() {
        if (Time.timeScale > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    

}   
