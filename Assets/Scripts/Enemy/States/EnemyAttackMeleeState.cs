using UnityEngine;

public class EnemyAttackMeleeState : EnemyAttackState {

    private BasicEnemy _enemy;
    public override void Enter(BasicEnemy owner) {
        _enemy = owner;
        _enemy.ResetRigidbodyVelocity();
        _enemy.Animator.Play(AnimationName.AttackMelee);
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
