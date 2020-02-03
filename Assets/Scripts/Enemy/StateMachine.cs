using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Type nextState;
    private Dictionary<Type, BaseState> _availableStates;

    public BaseState CurrentState { get; private set;}
    public event Action<BaseState> OnStateChanged;

    public void SetStates(Dictionary<Type, BaseState> states)
    {
        _availableStates = states;
        
    }

    

    void Update()
    {
        if (CurrentState == null)
        {
            CurrentState = _availableStates.Values.First();
        }

        InitPreviousState();


        //Переменная в которую мы возвращаем какой нибудь тип состояния. 
        //Или тот который и был или же какой-то новый, в который перешли
        //Заодно вызывая один кадр для апдейта       
        nextState = CurrentState?.Tick();
        SetPreviousState();

        if (nextState != null &&
            nextState != CurrentState?.GetType() &&
            _availableStates.ContainsKey(nextState))
        {
            SwitchToNewState(nextState);
        }
    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = _availableStates[nextState];
        
        OnStateChanged?.Invoke(CurrentState);
        
    }

    private void SetPreviousState()
    {
        CurrentState.PreviousState = CurrentState.GetType();
    }

    private void InitPreviousState()
    {
        if (CurrentState.PreviousState == null)
        {
            CurrentState.PreviousState = CurrentState.GetType();
        }
    }



#if UNITY_EDITOR
    void OnDrawGizmos() {
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.red;
        Handles.Label(transform.position + Vector3.up * 3, CurrentState?.GetType().Name , style);

    }
#endif
}
