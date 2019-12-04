using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Arrow : MonoBehaviour, IPooledObject
{
    private Rigidbody2D rb;
    private Vector3 direction;
    private bool hit;
    private Transform tempParent;
    private Collider2D colComponent;
    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        colComponent = GetComponent<Collider2D>();
        tempParent = transform.parent;


    }

    private void Update()
    {
        if(!hit)
            // Направляем стрелу туда,куда направлен вектор скорости
            transform.right = Vector2.Lerp(transform.right, rb.velocity, Time.deltaTime);
        
        

    }

    public void OnSpawnFromPool()
    {
        
    }

    public void OnReturnToPool()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = tempParent;
        transform.rotation = Quaternion.Euler(0,0,0);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        hit = true;
        transform.parent = col.transform;

        Destroy(gameObject, 1f);

        if (GameManager.Instance.healthContainer.ContainsKey(col.gameObject))
        {
            GameManager.Instance.healthContainer[col.gameObject].takeHit(GameSettings.Instance.ArrowDamage);
        }
        else
        {
            colComponent.enabled = false;
        }

        
        //ObjectPooler.Instance.ReturnToPool("Arrow", gameObject, 1f );

    }

    
    
    
}
