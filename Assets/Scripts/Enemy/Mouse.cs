using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    private bool inArea = false;
    public bool InArea { get => inArea; set => inArea = value; }

    private Vector3 playerPosition;
    public Vector3 PlayerPosition { get => playerPosition; set => playerPosition = value; }

    [SerializeField] private float speed;
    private SpriteRenderer sr;
    private Animator animator;
    private Health health;
    private bool playerCollision = false;
    private bool soundPlay = false;
    private bool explode = false;
    Collider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        coll = GetComponent<Collider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea)
        {
            if (!playerCollision)
            {
                animator.SetTrigger("InArea");
                if (!sr.flipX)                   
                    transform.position += Vector3.left * speed * Time.deltaTime;
                else
                    transform.position += Vector3.right * speed * Time.deltaTime;

                if (!soundPlay) {
                    AudioManager.Instance.Play("MouseSqueak");
                    soundPlay = true;
                }
                
                    

                //transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed);
            }

            if (!explode) {
                if (transform.position.x - PlayerPosition.x < 0.5f || playerCollision) {
                    coll.enabled = false;
                    Destroy(gameObject, 0.5f);
                    animator.SetTrigger("Explosion");
                    AudioManager.Instance.Play("FireballExplode");
                    explode = true;
                }

                
            }
            
                
        }

        if (health.HealthCount <= 0) {
            if (!explode) {
            
                coll.enabled = false;
                inArea = false;
                Destroy(gameObject, 0.5f);
                animator.SetTrigger("Explosion");
                AudioManager.Instance.Play("FireballExplode");
                explode = true;
            }

            
        }
        

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        playerCollision = col.gameObject == GameManager.Instance.player;
    }

}


