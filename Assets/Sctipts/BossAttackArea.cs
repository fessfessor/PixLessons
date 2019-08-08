using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackArea : MonoBehaviour
{
    private bool inArea = false;
    private bool isPlayer;
    private Vector3 playerPosition;
    [SerializeField] private GameObject BossParent;
    

    private IBoss bossBehaviour;

    private void Start()
    {
        bossBehaviour = BossParent.GetComponent<IBoss>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        isPlayer = col.gameObject.transform.name == "Player";

        if (isPlayer)
        {
            //Debug.Log("Player position - " + playerPosition);
            playerPosition = col.gameObject.transform.position;
            inArea = true;

            // Передаем параметры положения игрока и переменную что он зашел в территорию, чтобы было удобнее настраивать
            bossBehaviour.SetPlayerPosition(playerPosition, inArea, gameObject);

        }


    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isPlayer = col.gameObject.transform.name == "Player";
        if (isPlayer)
        {
            playerPosition = col.gameObject.transform.position;
            inArea = false;

            // Передаем параметры положения игрока и переменную что он зашел в территорию, чтобы было удобнее настраивать
            bossBehaviour.SetPlayerPosition(playerPosition, inArea, gameObject);

        }
    }

   

    
}
