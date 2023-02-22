public class EnemyHitState : IState<BasicEnemy> {

    protected BasicEnemy _enemy;

    public virtual void Enter(BasicEnemy owner) {
        _enemy = owner;
    }

    public virtual void ExecuteUpdate() {
    }

    public virtual void ExecuteFixedUpdate() {
    }

    public virtual void Exit() {
    }
}
