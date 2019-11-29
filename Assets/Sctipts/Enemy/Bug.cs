using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : SimplePatrol, IPooledObject
{
    public bool readyToPool = false;

    public override void Death() {
        base.isDeath = true;
        base.animator.SetTrigger("Death");
    }

    public void OnReturnToPool() {
        animator.WriteDefaultValues();
    }

    public void OnSpawnFromPool() {
        
    }


    public void DeathEnd() {
        readyToPool = true;
    }
}
