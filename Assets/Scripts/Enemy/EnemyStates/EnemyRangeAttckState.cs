using UnityEngine;

public class EnemyRangeAttckState : EnemyMeleeAttackState {
    private EnemyOfRange _enemyRange;

    public override void Enter(EnemyOfMelee owner) {
        _enemyRange = (EnemyOfRange)owner;
        _enemyRange.Animator.Play(AnimationName.Attack);
    }

    public override void ExecuteUpdate() {

        if (_enemyRange.IsEndCurrentAnimation(_enemyRange.Animator, AnimatorLayers.BaseLayer)) {
            _enemyRange.StateMachine.ChangeState(_enemyRange.IdleState);
        }
    }

    public override void ExecuteFixedUpdate() {

    }

    public override void Exit() {

    }
}
