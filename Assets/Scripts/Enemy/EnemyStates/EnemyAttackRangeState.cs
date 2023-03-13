public class EnemyAttackRangeState : EnemyAttackState {

    private BasicEnemy _enemyRange;

    public override void Enter(BasicEnemy owner) {
        _enemyRange = owner;
        _enemyRange.Animator.Play(AnimationName.AttackRange);
    }

    public override void Update() {
        if (_enemyRange.IsEndCurrentAnimation(_enemyRange.Animator, AnimatorLayers.BaseLayer)) {
            _enemyRange.StateMachine.ChangeState(_enemyRange.IdleState);
        }
    }

    public override void FixedUpdate() {
    }

    public override void Exit() {
    }
}
