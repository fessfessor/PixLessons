using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchingState : BaseState
{
    private SkeletonArcher _archer;
    
    private float timer;
    private bool isWatching;



    public WatchingState(SkeletonArcher _archer) : base(_archer.gameObject)
    {
        this._archer = _archer;
        
    }

    public override Type Tick()
    {
        timer += Time.deltaTime;

        if (timer > _archer.TurnFreq)
        {
            _archer.SwitchVision();
            timer = 0;
        }
            
            

        if (_archer.inShootArea)
        {
            return typeof(ShootingState);
        }
        else if (_archer.inAttackArea)
        {
            return typeof(HittingState);
        }
        
        return null;

    }

    
}
