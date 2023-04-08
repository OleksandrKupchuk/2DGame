public class EnemyRougeAttackRightHandUpState : EnemyAttackState {
    private EnemyRogue _enemy;

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyRogue)owner;
        _enemy.Animator.Play("Attack_RightHand_Up");
    }

    public override void Update() {
        if (_enemy.IsEndCurrentAnimation(_enemy.Animator, AnimatorLayers.BaseLayer)) {
            _enemy.StateMachine.ChangeState(_enemy.AttackJumpUp);
        }
    }

    public override void FixedUpdate() {
    }

    public override void Exit() {
        _enemy.DisableColliderRightKnife();
    }
}