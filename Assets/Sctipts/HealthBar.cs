using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    Vector3 localScale;

    void Start()
    {
        localScale = transform.localScale;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        localScale.x = gameObject.GetComponentInParent<Health>().health * 0.01f;
        transform.localScale = localScale;
    }
}
