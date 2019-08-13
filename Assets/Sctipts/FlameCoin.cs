using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameCoin : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject particle;
    private Animator animator;
    private ObjectPooler pooler;
    private ParticleSystem particleComponent;
    private SpriteRenderer sr;
    private Collider2D coll;
    

    private void Start() {
        // Добавляем себя в гейм менеджер
        GameManager.Instance.flameCoinContainer.Add(gameObject,this);
        GameManager.Instance.pooledObjectContainer.Add(gameObject, this);
        pooler = ObjectPooler.Instance;
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        particleComponent = particle.GetComponent<ParticleSystem>();
    }

   

    public void OnSpawnFromPool() {
        
    }

    public void TakeCoin() {
        //animator.SetTrigger("TakeFlame");
    }

    public void OnReturnToPool() {
        sr.enabled = true;
        coll.enabled = true;
        animator.WriteDefaultValues();
    }

    

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject == GameManager.Instance.player) {
            sr.enabled = false;
            coll.enabled = false;
            particleComponent.Play();
        }
    }
}
