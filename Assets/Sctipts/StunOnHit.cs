using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunOnHit : MonoBehaviour
{
    private Health health;
    private int currentHealth;
    private bool isDamaged;
    [SerializeField] float animationDelay;
    [SerializeField] float stunDuration;
    private Rigidbody2D rb;
    ObjectPooler pooler;
    GameObject stunIcon;
    private bool onStun;
    private Vector3 stunPosition;
    private Animator animator;
    private SpriteRenderer renderer2D;
    

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        renderer2D = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        onStun = false;
        isDamaged = false;
        pooler = ObjectPooler.Instance;
        
    }


    void Update()
    {
        if (!health) {
            health = GameManager.Instance.healthContainer[gameObject];
            currentHealth = health.HealthCount;
        }
            
        
        // Проверка на дамаг
        if (currentHealth > health.HealthCount && health.HealthCount > 0) {
            isDamaged = true;
            currentHealth = health.HealthCount;
        }
        else {
            isDamaged = false;
            currentHealth = health.HealthCount;
        }

        // Если задамажили, тормозим объект и показываем иконку стана
        if (isDamaged) {
            stunPosition = transform.position;
            StartCoroutine(Stunned());
            // Спавним иконку стана в топ-центр картинки
            stunIcon = pooler.SpawnFromPool("StunIcon", new Vector2(renderer2D.bounds.center.x, renderer2D.bounds.max.y), Quaternion.identity);
            StartCoroutine(stunIcon.GetComponent<StunIcon>().OnReturnToPool(stunIcon, animationDelay));

            
        }

        if (onStun) {
            
            transform.position = stunPosition;
        }

    }

    IEnumerator Stunned() {
        
        
        onStun = true;
        animator.enabled = false;
        yield return new WaitForSeconds(stunDuration);       
        onStun = false;
        animator.enabled = true;

       
    }


}
