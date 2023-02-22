public class EnemyAttackRangeState : EnemyAttackMeleeState {

    private EnemyOfRange _enemyRange;

    public override void Enter(BasicEnemy owner) {
        _enemyRange = (EnemyOfRange)owner;
        _enemyRange.delayAttack = 1.4f;
        _enemyRange.Animator.Play(AnimationName.AttackRange);
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
