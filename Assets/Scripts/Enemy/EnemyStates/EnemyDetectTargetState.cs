using UnityEngine;

public class EnemyDetectTargetState : IState<BasicEnemy> {

    private BasicEnemy _enemy;

    public virtual void Enter(BasicEnemy owner) {
        _enemy = owner;
    }

    public void ExecuteUpdate() {

        if (_enemy.FieldOfView.Target == null) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
            return;
        }

        if (_enemy.IsNeedLookOnPlayer()) {
            _enemy.Flip();
        }

        _enemy.distanceToTarget = Mathf.Abs(_enemy.transform.position.x - _enemy.FieldOfView.Target.transform.position.x);
        //Debug.Log(_distanceToTarget);
        if (_enemy.IsThereTargetInRangeOfAttack) {
            _enemy.ResetRigidbodyVelocity();
            _enemy.Animator.Play(AnimationName.Idle);
            _enemy.delayAttack -= Time.deltaTime;

            if (_enemy.delayAttack <= 0) {
                _enemy.StateMachine.ChangeState(_enemy.AttackState);
            }
        }
        else {
            _enemy.Animator.Play(AnimationName.Run);
        }
    }

    public virtual void ExecuteFixedUpdate() {
        if (!_enemy.IsThereTargetInRangeOfAttack) {
            _enemy.Move(_enemy.GetLocalScaleX);
        }
    }

    public virtual void Exit() {
    }
}
