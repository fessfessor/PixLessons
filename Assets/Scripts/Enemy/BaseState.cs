using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    protected GameObject gameObject;
    protected Transform transform;

    private Type previousState;
    public Type PreviousState { get => previousState; set => previousState = value; }

    public BaseState(GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    

    public abstract Type Tick();
    
}
