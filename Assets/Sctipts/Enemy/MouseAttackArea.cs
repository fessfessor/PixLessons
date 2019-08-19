using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAttackArea : MonoBehaviour
{
    private bool inArea = false;
    private Vector3 playerPosition;
    Mouse parent;

    private void Start()
    {
        parent = gameObject.transform.GetComponentInParent<Mouse>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        bool isPlayer = col.gameObject == GameManager.Instance.player;
        if (isPlayer && !inArea)
        {
            playerPosition = col.gameObject.transform.position;
            inArea = true;

            // Передаем параметры положения игрока и переменную что он зашел в территорию, чтобы было удобнее настраивать
            parent.InArea = inArea;
            parent.PlayerPosition = playerPosition;
        }
            

    }


   
}
