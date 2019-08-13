using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunIcon : MonoBehaviour, IPooledObject
{
    private ObjectPooler pooler;
    private Animator animator;

    private void Start() {
        GameManager.Instance.pooledObjectContainer.Add(gameObject, this);
        animator = GetComponent<Animator>();
        pooler = ObjectPooler.Instance;
    }
    

    public void OnSpawnFromPool() {
        throw new System.NotImplementedException();
    }

    public void OnReturnToPool() {
        animator.WriteDefaultValues();
    }
}
