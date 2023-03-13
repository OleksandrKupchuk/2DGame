public class EnemyDeadState : IState<BasicEnemy> {

    protected BasicEnemy _enemy;

    public virtual void Enter(BasicEnemy owner) {
        _enemy = owner;
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void Exit() {
    }
}
