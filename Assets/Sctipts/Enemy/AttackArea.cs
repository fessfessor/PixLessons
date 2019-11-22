using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private SimplePatrol simplePatrol;
   
    
    

    private void Start() {
        simplePatrol = transform.parent.GetComponent<SimplePatrol>();
       
       
    }


    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject == GameManager.Instance.player;
        if (isPlayer) {
            simplePatrol.Attack(true, col.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        bool isPlayer = col.gameObject == GameManager.Instance.player;
        if (isPlayer) {
            simplePatrol.Attack(false, null);
        }
    }

   

   

   
}
