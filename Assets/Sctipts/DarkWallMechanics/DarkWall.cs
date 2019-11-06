using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DarkWall : MonoBehaviour {
    [Header("Основные хар-ки")]
    [SerializeField] private float force = 1.0f;
    [SerializeField] private float stunTime = 3.0f;
    [SerializeField] private float forceIncreaseFrequency = 10.0f;
    [SerializeField] private float forceIncreaseDelta = 0.1f;
    
    
    private Rigidbody2D rb;
    private bool isMoving;
    private int enemy_koef = 0;
    private bool isStunning = false;
    private float count = 0.0f;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_KILL_ENEMY, OnEvent);
    }

    
    void Update()
    {
        if (!isStunning) {
            increasedSpeed();
            Move();
        }
            

        
    }

    //Со временем скорость стены повышается
    void increasedSpeed() {
        float time = Time.time;
        

        if(time > count) {
            count += forceIncreaseFrequency;
            force += forceIncreaseDelta;
        }
    }

    void Move() {
        rb.velocity = transform.right * Time.deltaTime * (force+enemy_koef);
    }


    // Если стена касается игрока, то мгновенно убивает.
    // Если стена касается врага, то убивает его, но из-за этого начинает двигаться быстрее. 
    // Увеличение скорости пропорционально силе врага. Можно убить об стену босса , но тогда стена будет двигаться как бешеная
    // Если же выстрелим по стене огнем, то это ненадолго ее застанит, но за это платим здоровьем
    private void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject == GameManager.Instance.player) {
            GameManager.Instance.healthContainer[GameManager.Instance.player].HealthCount += -9999;
        }else if(col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<Health>()!=null) {
            GameManager.Instance.healthContainer[col.gameObject].HealthCount += -9999;

            
            //Получим класс опасности
            force += (int)GameManager.Instance.enemyDangerContainer[col.gameObject] / 10f;
            


        }
        else if(col.gameObject.tag == "Fireball") {
            StartCoroutine(Stun());
        }
    }

    IEnumerator Stun() {
        isStunning = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        isStunning = false;

    }

    void DecreasedSpeed(object param) {
        // TODO Доделать событие
        //var
        //int dangerClass = GameManager.Instance.enemyDangerContainer[(gameObject)param]
    }

    void OnEvent(EVENT_TYPE eventType, Component sender, object param = null) {
        switch (eventType) {
            case EVENT_TYPE.PLAYER_KILL_ENEMY:
                DecreasedSpeed(param);
                break;           
            default:
                break;
        }
    }
}
