public class EnemyAttackRangeState : EnemyAttackState {

    private Enemy _enemyRange;

    public override void Enter(Enemy owner) {
        _enemyRange = owner;
        _enemyRange.Animator.Play(EnemyAnimationName.AttackRange);
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