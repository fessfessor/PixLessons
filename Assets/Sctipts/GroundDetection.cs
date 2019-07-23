using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    public bool isGrounded;

    private int countCollision;
    private Rigidbody2D rb;
    private Collider2D coll;
    private Vector3 downPoint;
    private RaycastHit2D ray;

    [SerializeField] private float minManualSet = 0.1f;
    private bool inManualState = false;
    private bool manualState = false;
    private WaitForSeconds delay;

    public int groundCount = 0;

    /*
    public bool IsGrounded {
        get { return inManualState ? manualState : isGrounded; }
        set { inManualState = true;
            manualState = value;
            StartCoroutine(ManualSet());
        }
    }

    private void Awake() {
        delay = new WaitForSeconds(minManualSet);
    }

    IEnumerator ManualSet() {
        yield return delay;
        inManualState = false;
    }


*/
    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        coll = gameObject.GetComponent<Collider2D>();
        
    }






    /*
    private void OnCollisionEnter2D(Collision2D col) {
        
        if (col.gameObject.CompareTag("Ground")) {
            // Дополнительная проверка лучом для того чтобы не было ситуаций, когда касаемся головой платформы и считаем что на земле
            // Берем нижнюю точку на коллайдере
            //ray = Physics2D.Raycast(new Vector3(coll.bounds.center.x, coll.bounds.min.y), Vector3.down, 0.2f);

            //if (ray.collider != null && ray.collider.gameObject.CompareTag("Ground")) {
              //  Debug.Log("OnCollisionEnter2D " + ray.transform.name);
               if (rb.velocity.y <  0.01f)
                isGrounded =  true;

            //}
        }
        
        
    }



    private void OnCollisionExit2D(Collision2D col) {
        
        if (col.gameObject.CompareTag("Ground")) {
            // ray = Physics2D.Raycast(new Vector3(coll.bounds.center.x, coll.bounds.min.y), Vector3.down, 0.2f);
            // if (ray.collider != null && ray.collider.gameObject.CompareTag("Ground")) {
            if (rb.velocity.y > 0.01)
                isGrounded = false;           
           // }

        }
    }
    */
   



    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Ground")) {
            //if (rb.velocity.y < 0.01) {
                isGrounded = true;
                groundCount++;
            //}
                
 
        }
    }

    private void OnTriggerExit2D(Collider2D col) {
        
        if (col.gameObject.CompareTag("Ground")) {           
            //if (rb.velocity.y > 0.01)
                groundCount--;

            if (groundCount == 0 ) 
                isGrounded = false;
                
        }
    }

}
