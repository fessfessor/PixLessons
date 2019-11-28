using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public static bool isPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject inventoryMenuUI;
    

    public Dictionary<GameObject, Health> healthContainer;
    public Dictionary<GameObject, FlameCoin> flameCoinContainer;
    public Dictionary<GameObject, GhostMove> ghostContainer;
    public Dictionary<GameObject, BuffReciever> buffRecieverContainer;
    public Dictionary<GameObject, IPooledObject> pooledObjectContainer;
    public Dictionary<GameObject, ItemComponent> itemsContainer;
    public Dictionary<GameObject, ENEMY_DANGER> enemyDangerContainer; 
    public UIController uiConroller;
    public ItemBase itemBase;
    public EnemyBase enemyBase;
    [HideInInspector]public PlayerInventory inventory;
    [HideInInspector]public GameObject player;
    [HideInInspector]public GameObject darkWall;

    private void Awake() {
        Instance = this;
        healthContainer = new Dictionary<GameObject, Health>();
        flameCoinContainer = new Dictionary<GameObject, FlameCoin>();
        ghostContainer = new Dictionary<GameObject, GhostMove>();
        buffRecieverContainer = new Dictionary<GameObject, BuffReciever>();
        pooledObjectContainer = new Dictionary<GameObject, IPooledObject>();
        itemsContainer = new Dictionary<GameObject, ItemComponent>();
        enemyDangerContainer = new Dictionary<GameObject, ENEMY_DANGER>();
        player = FindObjectOfType<Player>().gameObject;
        darkWall = FindObjectOfType<DarkWall>().gameObject;
        




    }

    private void Start() {
        // Игнорирования врагами коллизий с врагами
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("Enemy"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Traps"), LayerMask.NameToLayer("Trap"));
    }

    // Кнопка паузы
    public void OnClickPause() {
        if (Time.timeScale > 0) {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;          

        }else {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
           
        }
    }

    public void OnClickInventory() {
        if (inventoryMenuUI.active == false) {
            inventoryMenuUI.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;

        }else {
            inventoryMenuUI.SetActive(false);
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

        if(AudioListener.volume == 0) {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("GameSound", 1);            
        }
        else {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("GameSound", 0);           
        }

  
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        
    }

   






}

public enum ENEMY_DANGER { veryLow, low, medium, semiHight, hight, veryHight, boss = 10 }
