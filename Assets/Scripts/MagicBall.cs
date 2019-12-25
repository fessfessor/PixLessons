using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.PlayerLogic;
using UnityEngine;

public class MagicBall : MonoBehaviour , IPooledObject
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float lifeTime;      
    [SerializeField] private float deathAnimationDuration;

    private Animator animator;
    private Player player;    
    private ObjectPooler pooler;
    
    public float Force { get => force; set => force = value;}


    private void Start() {
        //GameManager.Instance.pooledObjectContainer.Add(gameObject, this);
        animator = GetComponent<Animator>();
        pooler = ObjectPooler.Instance;
    }


    

    public void SetImpulse(Vector2 direction, float force, Player player) {       
        this.player = player;
        // Непосредственно работаем с префабом
        rb.AddForce(direction* force, ForceMode2D.Impulse);       
    }

    

    private void OnTriggerEnter2D(Collider2D col) {
        // Маска для моих монеток, чтобы шар пролетал сквозь
        bool isNonTriggerObj = false;
        if (GameManager.Instance.flameCoinContainer.ContainsKey(col.gameObject) 
            || col.gameObject.transform.CompareTag("NonShootable")
            )
        isNonTriggerObj = true;
        
       
        if (col.gameObject != player.gameObject && !isNonTriggerObj) {
            // Тормозим объект в месте попадания
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0;
            animator.SetBool("Explosive", true);
            AudioManager.Instance.Play("FireballExplode");
            pooler.ReturnToPool("MagicBall", gameObject, 0.4f);

            //"Кровавая механика", размещаем информацию о событии попадания или промаха
            if (col.gameObject.tag == "Enemy")
                EventManager.Instance.PostNotification(EVENT_TYPE.BLD_BALL_HIT, this);
            else
                EventManager.Instance.PostNotification(EVENT_TYPE.BLD_BALL_MISS, this);


        }
    }


    public void OnSpawnFromPool() {

    }

    public void OnReturnToPool() {
        Debug.Log("OnReturnToPool!");
        animator.WriteDefaultValues();
        rb.gravityScale = 1;
    }
}


