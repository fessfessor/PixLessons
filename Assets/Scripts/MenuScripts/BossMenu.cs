using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerLogic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BossMenu : MonoBehaviour
{
    [SerializeField] private GameObject BossNebula;
    [SerializeField] private GameObject canvas;
    [SerializeField] GameObject BossObject;
    [SerializeField] GameObject BossAltar;
    [SerializeField] GameObject LeftAltarStatue;
    [SerializeField] GameObject RightAltarStatue;
    [SerializeField] GameObject RightBorderStatue;
    [SerializeField] GameObject LeftBorderStatue;
    private RectTransform canvasRectT;
    private Vector2 screenPoint;
    private SpriteRenderer sr;
    private RectTransform rectTranform;
    private Player player;
    private CameraShake shaker;
    private IBoss boss;

    private Animator leftAnim;
    private Animator rightAnim;



    private void Start()
    {
        player = FindObjectOfType<Player>();
        shaker = player.GetComponent<CameraShake>();
        canvasRectT = canvas.GetComponent<RectTransform>();
        sr = BossNebula.GetComponent<SpriteRenderer>();
        rectTranform = gameObject.GetComponent<RectTransform>();

        // Берем у объекта босса компонент с поведение при спавне и т.п.
        boss = BossObject.GetComponent<IBoss>();

        leftAnim = LeftAltarStatue.GetComponent<Animator>();
        rightAnim = RightAltarStatue.GetComponent<Animator>();
    }

    void Update()
    {
        //Спавним меню в центре и сверху у небулы
        screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(sr.bounds.center.x-1f, sr.bounds.max.y - 0.5f));
        rectTranform.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
    }

    public void SpawnSimpleBoss()
    {
        // Трясем камеру, опасность же
        shaker.Shake();       
        boss.OnSpawnSimpleBoss();
        StartCoroutine(StartFightWithBoss());
    }

    public void SpawnBloodBoss()
    {
        shaker.Shake();
        boss.OnSpawnBloodBoss();
        StartCoroutine(StartFightWithBoss());
    }

    public void SpawnLightBoss()
    {
        shaker.Shake();
        boss.OnSpawnLightBoss();
        StartCoroutine(StartFightWithBoss());
    }

    

    IEnumerator  StartFightWithBoss()
    {
        AudioManager.Instance.Mute("Theme_" + SceneManager.GetActiveScene().buildIndex, true);
        AudioManager.Instance.Play("BossFight");
        leftAnim.SetTrigger("StartFight");
        rightAnim.SetTrigger("StartFight");
    yield return new WaitForSeconds(1f);      
        BossAltar.SetActive(false);
        RightBorderStatue.SetActive(true);
        LeftBorderStatue.SetActive(true);
    }

}
