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
    private bool isPlayer = false;
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
            if (!isPlayer)
            {
                animator.SetTrigger("InArea");
                transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed);
            }
            

            if (transform.position == playerPosition || isPlayer)
            {
                coll.enabled = false;
                Destroy(gameObject, 0.5f);
                animator.SetTrigger("Explosion");
            }
                
        }

        if(health.HealthCount <= 0)
        {
            coll.enabled = false;
            inArea = false;
            Destroy(gameObject, 0.5f);
            animator.SetTrigger("Explosion");
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        isPlayer = col.gameObject.transform.name == "Player";
    }

}


