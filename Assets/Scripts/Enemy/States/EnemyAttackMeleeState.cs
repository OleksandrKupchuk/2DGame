using UnityEngine;

public class EnemyAttackMeleeState : EnemyAttackState {

    private Enemy _enemy;
    public override void Enter(Enemy owner) {
        _enemy = owner;
        _enemy.ResetRigidbodyVelocity();
        _enemy.Animator.Play(EnemyAnimationName.AttackMelee);
    }

    public override void Update() {
        if (_enemy.IsEndCurrentAnimation(_enemy.Animator, AnimatorLayers.BaseLayer)) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public override void FixedUpdate() {
    }

    public override void Exit() {
    }
}
