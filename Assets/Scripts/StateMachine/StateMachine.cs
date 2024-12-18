public class StateMachine<T> {
    public T owner;
    public IState<T> CurrentState { get; private set; }

    public StateMachine(T owner) {
        CurrentState = null;
        this.owner = owner;
    }

    public void Update() {
        if(CurrentState != null) {
            //Debug.Log("update state machine");
            CurrentState.Update();
        }
    }

    public void FixedUpdate() {
        if (CurrentState != null) {
            //Debug.Log("update state machine");
            CurrentState.FixedUpdate();
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
