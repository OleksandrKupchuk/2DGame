using UnityEngine;

public class EnemyIdleState : IState<Enemy> {

    protected Enemy _enemy;
    private float _timer;

    public virtual void Enter(Enemy owner) {
        _enemy = owner;
        _enemy.ResetRigidbodyVelocity();
        _enemy.Animator.Play(AnimationName.Idle);
        _timer = Random.Range(_enemy.Config.timerMinIdle, _enemy.Config.timerMaxIdle);
    }

    public virtual void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= 0) {
            _enemy.StateMachine.ChangeState(_enemy.RunState);
        }
        if (_enemy.FieldOfView.Target != null) {
            _enemy.StateMachine.ChangeState(_enemy.DetectTarget);
        }
    }

    public virtual void FixedUpdate() {
    }

    public virtual void Exit() {
    }
}
