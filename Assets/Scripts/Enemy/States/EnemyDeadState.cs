public class EnemyDeadState : IState<Enemy> {

    protected Enemy _enemy;

    public virtual void Enter(Enemy owner) {
        _enemy = owner;
        _enemy.Animator.Play(AnimationName.Dead);
        _enemy.FieldOfView.gameObject.SetActive(false);
    }

    public virtual void Update() {
    }

    public virtual void FixedUpdate() {
    }

    public virtual void Exit() {
    }
}