using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMenu : MonoBehaviour
{
    [SerializeField] private GameObject BossNebula;
    [SerializeField] private GameObject canvas;
    private RectTransform canvasRectT;
    private Vector2 screenPoint;
    private SpriteRenderer sr;
    private RectTransform rectTranform;


    private void Start()
    {
        canvasRectT = canvas.GetComponent<RectTransform>();
        sr = BossNebula.GetComponent<SpriteRenderer>();
        rectTranform = gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        //Спавним меню в центре и сверху у небулы
        screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(sr.bounds.center.x-1f, sr.bounds.max.y - 0.5f));

        rectTranform.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
    }

    public void SpawnSimpleBoss()
    {
        Debug.Log("SpawnSimpleBoss");
    }

    public void SpawnBloodBoss()
    {
        Debug.Log("SpawnBloodBoss");
    }

    public void SpawnLightBoss()
    {
        Debug.Log("SpawnLightBoss");
    }

    
}
