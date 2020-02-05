using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagicBall : MonoBehaviour, IPooledObject
{
    private Animator animator;
    [SerializeField] private Collider2D ballCollider;
    [SerializeField] private int damage;
    [SerializeField] private bool CanReflectBySword;    
    [SerializeField] private string poolName;    
    ObjectPooler pooler;

    private void Start() {
        
        pooler = ObjectPooler.Instance;
        animator = GetComponent<Animator>();
        pooler = ObjectPooler.Instance;
        
    }



    private void OnTriggerEnter2D(Collider2D col) {
        bool isPlayer = col.gameObject == GameManager.Instance.player;
        

        if (isPlayer) {
            GameManager.Instance.healthContainer[col.gameObject].takeHit(damage);
            ExplodeBall();   
        }
        else if (col.gameObject.CompareTag("HeroSword") && CanReflectBySword) { // Если шлепаем мечом , например по прзрачному шару, то уничтожаем его
            ExplodeBall();            
        }

    }

    void ExplodeBall() {
        animator.SetTrigger("isExplosion");
        ballCollider.enabled = false;
        pooler.ReturnToPool(poolName, gameObject, 0.5f);
    }

    public void OnSpawnFromPool() {

    }

    public void OnReturnToPool() {
        ballCollider.enabled = true;
        animator.WriteDefaultValues();
    }
}
