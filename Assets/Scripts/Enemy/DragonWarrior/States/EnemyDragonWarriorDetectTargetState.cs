using UnityEngine;

public class EnemyDragonWarriorDetectTargetState : EnemyDetectTargetState {

    private EnemyDragorWarrior _enemy;

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyDragorWarrior)owner;
    }

    public override void Update() {

        _enemyDetectLogic.CheckHasTargetAndChangeToIdleState(_enemy);

        _enemyDetectLogic.CheckNeedFlipAndFlip(_enemy);

        _enemy.distanceToTarget = Mathf.Abs(_enemy.transform.position.x - _enemy.FieldOfView.Target.transform.position.x);

        if (_enemy.IsThereTargetInRangeOfDistance(_enemy.StrikeAttackDistance)) {

            _enemy.delayStrikeAttack -= Time.deltaTime;
            _enemyDetectLogic.ChangeToStateAttackAfterDelay(_enemy, _enemy.StrikeState, _enemy.delayStrikeAttack);
        }
        else if (_enemy.IsThereTargetInRangeOfDistance(_enemy.AttackRangeDistance)) {

            _enemy.Config.delayAttack -= Time.deltaTime;
            _enemyDetectLogic.ChangeToStateAttackAfterDelay(_enemy, _enemy.AttackState, _enemy.Config.delayAttack);
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
