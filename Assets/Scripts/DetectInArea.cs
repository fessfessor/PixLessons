using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInArea : MonoBehaviour
{

    [SerializeField] float checkFreq;
    [SerializeField] float checkRadius;
    [SerializeField] GameObject searchTarget;

    private Collider2D[] hits;

    [HideInInspector] public GameObject target;
    [HideInInspector] public bool inArea;
    [HideInInspector] public Vector3 targetPosition;


    void Start()
    {
        StartCoroutine(CheckArea());
    }

    
   


    IEnumerator CheckArea() {
        while (true) {
            yield return new WaitForSeconds(checkFreq);

            //Находим в радиусе цель
            hits = Physics2D.OverlapCircleAll(transform.position, checkRadius);
            if (hits.Length > 0) {
                for (int i = 0; i < hits.Length; i++) {
                    if (hits[i].gameObject == searchTarget) {
                        target = hits[i].gameObject;
                        targetPosition = target.transform.position;                       
                        break;
                    }
                    else 
                        target = null;                   
                }
            }
            else 
                target = null;
            
        }

    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, checkRadius);

    }
}
