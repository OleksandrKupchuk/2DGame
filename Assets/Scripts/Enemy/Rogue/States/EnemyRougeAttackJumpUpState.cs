public class EnemyRougeAttackJumpUpState : EnemyAttackState {
    private EnemyRogue _enemy;

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyRogue)owner;
        _enemy.Animator.Play("Attack_Jump_Up");
    }

    public override void Update() {
        if (_enemy.IsEndCurrentAnimation(_enemy.Animator, AnimatorLayers.BaseLayer)) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public override void FixedUpdate() {
    }

    public override void Exit() {
        _enemy.DisableColliderLeftKnife();
        _enemy.DisableColliderRightKnife();
    }
}