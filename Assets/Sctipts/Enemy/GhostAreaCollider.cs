using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostAreaCollider : MonoBehaviour
{
    [SerializeField] GameObject parent;
    private float shootFrequency;
    private GhostMove parentGhostMove;
    private bool inArea;


    private void Start() {
        parentGhostMove = parent.GetComponent<GhostMove>();
        shootFrequency = parentGhostMove.shootFrequency;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";

        if (isPlayer) {
            inArea = true;
            //Вызываем каст файрбола
            StartCoroutine(parentShoot(col.gameObject));
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";

        if (isPlayer) {
            inArea = false;
            StopAllCoroutines();
        }
    }

   

    IEnumerator parentShoot(GameObject player) {
        while (inArea) {
            parentGhostMove.StopAndShoot(player);
            yield return new WaitForSeconds(shootFrequency);

        }
        yield break;

    }

    
}
