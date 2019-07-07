using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRiseArea : MonoBehaviour
{
    [SerializeField] GameObject parent;
    private SimplePatrol simplePatrol;


    private void Start() {
        simplePatrol = parent.GetComponent<SimplePatrol>();
        
    }
    //Если в территорию скелета забрел игрок, то передаем скелету игформацию о том что пора вставать
    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject.transform.name == "Player";

        if (isPlayer) {
            simplePatrol.Rise();
        }
    }
}
