using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> {
    public IState<T> CurrentState { get; private set; }
    public T owner;

    public StateMachine(T owner) {
        CurrentState = null;
        this.owner = owner;
    }

    public void Update() {
        if(CurrentState != null) {
            //Debug.Log("update state machine");
            CurrentState.ExecuteUpdate();
        }
    }

    public void FixedUpdate() {
        if (CurrentState != null) {
            //Debug.Log("update state machine");
            CurrentState.ExecuteFixedUpdate();
        }
    }

    public void ChangeState(IState<T> newState) {
        if(CurrentState != null) {
            CurrentState.Exit();
        }
        CurrentState = newState;
        CurrentState.Enter(owner);
    }
}
