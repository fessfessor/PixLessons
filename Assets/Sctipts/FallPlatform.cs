using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    Rigidbody2D rb;
   
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            
            PlatformManager.Instance.StartCoroutine("SpawnPlatform", new Vector2(transform.position.x, transform.position.y));    
            StartCoroutine(DropPlatform());
            
        }
    }

   

    IEnumerator DropPlatform() {
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = false;
        Destroy(gameObject, 2f);
    }

   
}
