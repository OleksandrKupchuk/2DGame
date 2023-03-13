using UnityEngine;

public class EnemyMeleeDetectTargetState : EnemyDetectTargetState {

    private EnemyOfMelee _enemy;

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyOfMelee)owner;
    }

    public override void Update() {

        CheckHasTargetAndChangeToIdleState(_enemy);

        CheckNeedFlipAndFlip(_enemy);

        CalculationDistanceToTarget(_enemy);

        CheckIsThereTargetInRangeOfAttackAndAttackOrRun(_enemy.AttackMeleeDistance, _enemy, _enemy.AttackState);
    }

    public override void FixedUpdate() {
        CheckIsThereTargetInRangeOfAttackAndAttackOrRun(_enemy.AttackMeleeDistance, _enemy, _enemy.AttackState);
    }

    public override void Exit() {
        RefreshDelayForDifferentAttacks(_enemy);
    }
}