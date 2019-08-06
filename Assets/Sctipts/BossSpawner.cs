using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossMenu;
    private bool isPlayer = false;


    private void OnTriggerEnter2D(Collider2D col)
    {
        isPlayer = col.gameObject.transform.name == "Player";
        if (isPlayer)
        {
            bossMenu.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isPlayer = col.gameObject.transform.name == "Player";
        if (isPlayer)
        {
            bossMenu.SetActive(false);
        }
    }
}
