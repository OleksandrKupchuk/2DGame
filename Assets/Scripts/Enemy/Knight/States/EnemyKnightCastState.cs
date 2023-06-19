public class EnemyKnightCastState : IState<Enemy> {
    private EnemyKnight _enemy;

    public void Enter(Enemy owner) {
        _enemy = (EnemyKnight)owner;
    }

    public void Update() {
    }

    public void FixedUpdate() {
    }

    public void Exit() {
    }
}
