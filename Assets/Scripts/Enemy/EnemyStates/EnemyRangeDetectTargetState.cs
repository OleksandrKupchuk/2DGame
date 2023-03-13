using UnityEngine;

public class EnemyRangeDetectTargetState : EnemyDetectTargetState {

    private EnemyOfRange _enemy;

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyOfRange)owner;
    }

    public override void Update() {

        CheckHasTargetAndChangeToIdleState(_enemy);

        CheckNeedFlipAndFlip(_enemy);

        CalculationDistanceToTarget(_enemy);

        CheckIsThereTargetInRangeOfAttackAndAttackOrRun(_enemy.AttackRangeDistance, _enemy, _enemy.AttackState);
    }

    public override void FixedUpdate() {
        CheckIsThereTargetInRangeOfAttackAndAttackOrRun(_enemy.AttackRangeDistance, _enemy, _enemy.AttackState);
    }

    public override void Exit() {
        RefreshDelayForDifferentAttacks(_enemy);
    }
}