using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

     void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name.Equals("Player")) {
            Debug.Log("Jump");
            PlatformManager.Instance.StartCoroutine("SpawnPlatform", new Vector2(transform.position.x, transform.position.y));
            Invoke("DropPlatform", 0.5f);
            Destroy(gameObject, 2f);
                        
        }
    }

    void DropPlatform() {
        rb.isKinematic = false;
    }
}
