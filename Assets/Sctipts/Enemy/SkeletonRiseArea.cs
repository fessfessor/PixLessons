using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRiseArea : MonoBehaviour
{
    
    private SimplePatrol simplePatrol;


    private void Start() {
        simplePatrol = transform.parent.GetComponent<SimplePatrol>();
        
    }
    //Если в территорию скелета забрел игрок, то передаем скелету игформацию о том что пора вставать
    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject ==  GameManager.Instance.player;

        if (isPlayer) {
            simplePatrol.Rise();
        }
    }
}
