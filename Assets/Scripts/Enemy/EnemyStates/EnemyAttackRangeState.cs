using UnityEngine;

public class EnemyAttackRangeState : IState<EnemyOfMelee> {
    private EnemyOfRange _enemyRange;

    public void Enter(EnemyOfMelee owner) {
        _enemyRange = (EnemyOfRange)owner;
        _enemyRange.Animator.Play(AnimationName.AttackMelee);
    }

    public void ExecuteUpdate() {

        if (_enemyRange.IsEndCurrentAnimation(_enemyRange.Animator, AnimatorLayers.BaseLayer)) {
            _enemyRange.StateMachine.ChangeState(_enemyRange.IdleState);
        }
    }

    public void ExecuteFixedUpdate() {

    }

    public void Exit() {

    }
}
