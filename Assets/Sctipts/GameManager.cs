using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public static bool isPaused = false;
    [SerializeField] GameObject pauseMenuUI;


    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, FlameCoin> flameCoinContainer;
    public Dictionary<GameObject, GhostMove> ghostContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public UIController uiConroller;

    private void Awake() {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        flameCoinContainer = new Dictionary<GameObject, FlameCoin>();
        ghostContainer = new Dictionary<GameObject, GhostMove>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();




    }

    // Кнопка паузы
    public void OnClickPause() {
        if (Time.timeScale > 0) {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;

        }
        else {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    public void Resume() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        
    }

    public void LoadMainMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void Sound() {

    }

    


    

}   
