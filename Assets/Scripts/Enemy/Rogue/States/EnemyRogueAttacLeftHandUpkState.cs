using UnityEngine;

public class EnemyRogueAttacLeftHandUpkState : EnemyAttackState {
    private EnemyRogue _enemy;
    private int _chanceComboAttck = 100;
    private int _randomChance;

    private bool CanNextAttack { get => _enemy.IsEndCurrentAnimation(_enemy.Animator, AnimatorLayers.BaseLayer) && _randomChance <= _chanceComboAttck; }
    private bool CantNextAttack { get => _enemy.IsEndCurrentAnimation(_enemy.Animator, AnimatorLayers.BaseLayer) && _randomChance > _chanceComboAttck; }

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyRogue)owner;
        _randomChance = Random.Range(0, 100);
        _enemy.Animator.Play("Attack_LeftHand_Up");
    }

    public override void Update() {
        if (CanNextAttack) {
            _enemy.StateMachine.ChangeState(_enemy.AttackRightHandUp);
        }
        else if(CantNextAttack) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public override void FixedUpdate() {
    }

    public override void Exit() {
        _enemy.DisableColliderLeftKnife();
    }
}