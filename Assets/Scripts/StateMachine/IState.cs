public interface IState<T>
{
    public void Enter(T owner);
    public void ExecuteUpdate();
    public void ExecuteFixedUpdate();
    public void Exit();
}
