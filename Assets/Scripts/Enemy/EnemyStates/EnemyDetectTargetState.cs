using UnityEngine;

public class EnemyDetectTargetState : IState<EnemyOfMelee> {
    private EnemyOfMelee _enemy;
    protected float _distanceToTarget;

    private bool IsCloseToTagret { get => _distanceToTarget <= _enemy.DistanceToMeleeAttack; }

    public void Enter(EnemyOfMelee owner) {
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

        _distanceToTarget = Mathf.Abs(_enemy.transform.position.x - _enemy.FieldOfView.Target.transform.position.x);
        //Debug.Log(_distanceToTarget);
        if (IsCloseToTagret) {
            _enemy.ResetRigidbodyVelocity();
            _enemy.Animator.Play(AnimationName.Idle);
            _enemy.delayAttack -= Time.deltaTime;

            if (_enemy.delayAttack <= 0) {
                _enemy.StateMachine.ChangeState(_enemy.AttackMeleeState);
            }
        }
        else {
            _enemy.Animator.Play(AnimationName.Run);
        }
    }

    public void ExecuteFixedUpdate() {
        if (!IsCloseToTagret) {
            _enemy.Move(_enemy.GetLocalScaleX);
        }
    }

    public void Exit() {
    }
}
