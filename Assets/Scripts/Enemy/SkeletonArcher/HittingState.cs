using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingState : BaseState {
    private SkeletonArcher _archer;
    private float timer;

    public HittingState(SkeletonArcher _archer) : base(_archer.gameObject)
    {
        this._archer = _archer;
        timer = _archer.attackFreq;
    }

    public override Type Tick()
    {
        timer += Time.deltaTime;

        if (timer > _archer.attackFreq)
        {
            Hitting();
            timer = 0;
        }

        if (!_archer.inAttackArea)
            return typeof(WatchingState);

        return null;

    }

    private void Hitting()
    {
        _archer.anim.SetTrigger("Hit");
    }
}
