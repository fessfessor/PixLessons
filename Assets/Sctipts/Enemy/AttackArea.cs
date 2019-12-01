using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private IEnemy parentComponent;
    private Collider2D col;
    
    

    private void Start() {
        parentComponent = transform.parent.GetComponent<IEnemy>();
        col = GetComponent<Collider2D>();

    }


    private void OnTriggerEnter2D(Collider2D col) {
        var isPlayer = col.gameObject == GameManager.Instance.player;
        if (isPlayer) {
            parentComponent.Attack(true, col.gameObject);
            
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        var o = col.gameObject;
        var isPlayer = o == GameManager.Instance.player || o == GameManager.Instance.playerTrigger;
        if (isPlayer) {
            parentComponent.Attack(false, null);
        }
    }

   

   
}
