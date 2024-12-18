public interface IState<T> {
    public void Enter(T owner);
    public void Update();
    public void FixedUpdate();
    public void Exit();
}
