using UnityEngine;

public class EnemyIdleState : IState<BasicEnemy> {

    protected BasicEnemy _enemy;
    private float _timer;

    public virtual void Enter(BasicEnemy owner) {
        _enemy = owner;
        _enemy.ResetRigidbodyVelocity();
        _enemy.Animator.Play(AnimationName.Idle);
        _timer = 3f;
        //Debug.Log("EnemyIdle state enter");
    }

    public virtual void ExecuteUpdate() {
        _timer -= Time.deltaTime;
        if (_timer <= 0) {
            _enemy.StateMachine.ChangeState(_enemy.RunState);
        }
        if (_enemy.FieldOfView.Target != null) {
            _enemy.StateMachine.ChangeState(_enemy.DetectTarget);
        }
    }

    public virtual void ExecuteFixedUpdate() {
    }

    public virtual void Exit() {
    }
}
