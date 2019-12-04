using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingState : BaseState {
    private SkeletonArcher _archer;
    private float timer;

    public ShootingState(SkeletonArcher _archer) : base(_archer.gameObject)
    {
        this._archer = _archer;
        timer = _archer.ShootFreq;


    }

    public override Type Tick()
    {
        timer += Time.deltaTime;

        if (timer > _archer.ShootFreq)
        {
            Shooting();
            timer = 0;
        }
            

        if (!_archer.inShootArea) 
        {
            return typeof(WatchingState);
        }
        else if (_archer.inAttackArea)
        {
            return typeof(HittingState);
        }
            
            

        return null;
    }

    private void Shooting() {

        _archer.anim.SetTrigger("Shoot");
   
    }
}
