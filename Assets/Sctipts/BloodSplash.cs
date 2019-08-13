using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplash : MonoBehaviour, IPooledObject
{
    private Animator animator;

    public void OnReturnToPool() {
        animator.WriteDefaultValues();
    }

    public void OnSpawnFromPool() {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        GameManager.Instance.pooledObjectContainer.Add(gameObject, this);
        animator = GetComponent<Animator>();
    }

    
}
