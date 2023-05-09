using UnityEngine;

public class EnemyRogueDetectTargetState : EnemyDetectTargetState {
    private EnemyRogue _enemy;

    public override void Enter(Enemy owner) {
        _enemy = (EnemyRogue)owner;
    }

    public override void Update() {
        _enemyDetectLogic.CheckHasTargetAndChangeToIdleState(_enemy);

        _enemyDetectLogic.CheckNeedFlipAndFlip(_enemy);

        _enemyDetectLogic.CalculationDistanceToTarget(_enemy);

        if (_enemy.IsThereTargetInRangeOfDistance(_enemy.Config.distanceMeleeAttack)) {
            _enemy.delayAttack -= Time.deltaTime;
            _enemyDetectLogic.ChangeToStateAttackAfterDelay(_enemy, _enemy.AttackState, _enemy.delayAttack);
        }
        else {
            _enemy.Animator.Play(AnimationName.Run);
        }
    }

    public override void FixedUpdate() {
        _enemyDetectLogic.MoveIfPlayRunAnimation(_enemy);
    }

    public override void Exit() {
        _enemyDetectLogic.RefreshDelayForDifferentAttacks(_enemy);
    }
}