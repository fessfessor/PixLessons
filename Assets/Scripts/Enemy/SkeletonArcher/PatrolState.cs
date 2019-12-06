using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private SkeletonArcher _archer;
    private Rigidbody2D rb;

    public PatrolState(SkeletonArcher _archer) : base(_archer.gameObject)
    {
        this._archer = _archer;
        rb = transform.GetComponent<Rigidbody2D>();
        
    }

    public override Type Tick()
    {
        Moving();

        if (_archer.inShootArea)
        {
            _archer.anim.SetBool("isPatroling", false);
            return typeof(ShootingState);
        }
        if (_archer.inAttackArea)
        {
            _archer.anim.SetBool("isPatroling", false);
            return typeof(HittingState);
        }

            

        return null;

    }

    private void Moving()
    {
        _archer.anim.SetBool("isPatroling", true);

        if (_archer.isRightDirection && !_archer.isDeath) {
            rb.velocity = Vector2.right * _archer.speed;
            if (transform.position.x > _archer.rightBorderPosition.x) {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _archer.isRightDirection = false;
            }
        }
        else if (!_archer.isRightDirection && !_archer.isDeath) {
            rb.velocity = Vector2.left * _archer.speed;

            if (transform.position.x < _archer.leftBorderPosition.x) {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _archer.isRightDirection = true;
            }
        }
        else if (_archer.isDeath)
            rb.velocity = Vector3.zero;
    }
}
