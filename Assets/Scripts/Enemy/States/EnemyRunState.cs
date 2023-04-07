using UnityEngine;

public class EnemyRunState : IState<BasicEnemy> {

    protected BasicEnemy _enemy;
    private float _timer;

    public virtual void Enter(BasicEnemy owner) {
        _enemy = owner;
        _enemy.Animator.Play(AnimationName.Run);
        _timer = Random.Range(_enemy.Config.timerMinRun, _enemy.Config.timerMaxRun);
        _enemy.Flip();
    }

    public virtual void Update() {
        _timer -= Time.deltaTime;
        if (_timer <= 0) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
        }
        if (_enemy.FieldOfView.Target != null) {
            _enemy.StateMachine.ChangeState(_enemy.DetectTarget);
        }
    }

    public virtual void FixedUpdate() {
        _enemy.Move(_enemy.GetDirectionX);
    }

    public virtual void Exit() {
        _enemy.ResetRigidbodyVelocity();
    }
}
