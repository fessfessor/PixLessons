using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.0f;
    public float force = 1.0f;
    public Rigidbody2D rb;   
    bool doubleJumpAllowed = false;
    public float minHeight = -50.0f;
    public SpriteRenderer[] renderers;
    public GroundDetection groundD;
    

    public float timeRemaining = 3.0f;

    GameObject plat;
    
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();

        plat = GameObject.Find("Platforms");
        renderers = plat.GetComponentsInChildren<SpriteRenderer>();      

    }

    // Update is called once per frame
    void Update()
    {

        if (groundD.isGrounded) {
            doubleJumpAllowed = true;
        }
        
        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        } else if (Input.GetKey(KeyCode.D)) {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }

        // Обработка прыжка и двогйного прыжка
        if ( Input.GetKeyDown(KeyCode.Space) && groundD.isGrounded) {
            Jump();          
        }else if(doubleJumpAllowed && Input.GetKeyDown(KeyCode.Space)) {
            Jump();
            doubleJumpAllowed = false;
        }

        // телепортация обратно на платформу
        if(transform.position.y < minHeight) {
            rb.velocity = new Vector2(0,0);
            transform.position = new Vector2(0, 0);
        }


   
    }

    /*
    private void OnCollisionEnter2D(Collision2D col) {
        // Смена цвета платформы при прыжке на нее
        if (col.gameObject.CompareTag("Platform")) {
            col.gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

    }

    void insanePlatforms(SpriteRenderer[] renderers) {
        //Рандомайзер цвета  
        Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

    }
    */

    //Метод прыжка
    void Jump() {
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

   


}
