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
            SwitchVision();
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

    private void SwitchVision()
    {
        transform.rotation = Quaternion.Euler(0, transform.rotation == Quaternion.Euler(0, 0, 0) ? 180 : 0, 0);
        _archer.isRightDirection = _archer.transform.rotation.y <= 0;

    }
}
