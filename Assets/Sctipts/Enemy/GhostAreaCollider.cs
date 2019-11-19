using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAreaCollider : MonoBehaviour
{
    
    private float shootFrequency;
    private GhostMove parentGhostMove;
    private bool inArea;
    private bool readyToShoot;
    private bool drawGizmos;


    private void Start() {

        readyToShoot = true;
        inArea = false;
        parentGhostMove = transform.parent.GetComponent<GhostMove>();
        // частота стрельбы
        shootFrequency = parentGhostMove.shootFrequency;
        drawGizmos = parentGhostMove.drawGizmos;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";
        if(isPlayer)
            inArea = true;

        if (isPlayer && readyToShoot) {           
            //Вызываем каст файрбола
            StartCoroutine(parentShoot(col.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";
        if (isPlayer) 
            inArea = false;
            
        
    }



    IEnumerator parentShoot(GameObject player) {
        //Стреляем пока игрок находится в территории патрулирования
        while (inArea && readyToShoot) {           
            parentGhostMove.StopAndShoot(player);
            readyToShoot = false;
            yield return new WaitForSeconds(shootFrequency);
            readyToShoot = true;

        }
        yield break;

    }



    private void OnDrawGizmos() {
        if (drawGizmos) {
            Collider2D collider2D = gameObject.GetComponent<Collider2D>();
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(collider2D.bounds.center, collider2D.bounds.size);
        }
    }


}
