using UnityEngine;

public class EnemyMeleeDetectTargetState : EnemyDetectTargetState {

    private EnemyOfMelee _enemy;

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyOfMelee)owner;
    }

    public override void Update() {

        _enemyDetectLogic.CheckHasTargetAndChangeToIdleState(_enemy);

        _enemyDetectLogic.CheckNeedFlipAndFlip(_enemy);

        _enemyDetectLogic.CalculationDistanceToTarget(_enemy);

        _enemyDetectLogic.CheckIsThereTargetInRangeOfAttackAndAttackOrRun(_enemy.AttackMeleeDistance, _enemy, _enemy.AttackState);
    }

    public override void FixedUpdate() {
        _enemyDetectLogic.CheckIsThereTargetInRangeOfAttackAndAttackOrRun(_enemy.AttackMeleeDistance, _enemy, _enemy.AttackState);
    }

    public override void Exit() {
        _enemyDetectLogic.RefreshDelayForDifferentAttacks(_enemy);
    }
}