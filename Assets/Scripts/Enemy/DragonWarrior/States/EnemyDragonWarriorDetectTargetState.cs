using UnityEngine;

public class EnemyDragonWarriorDetectTargetState : EnemyDetectTargetState {

    private EnemyDragorWarrior _enemy;

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyDragorWarrior)owner;
    }

    public override void Update() {

        CheckHasTargetAndChangeToIdleState(_enemy);

        CheckNeedFlipAndFlip(_enemy);

        _enemy.distanceToTarget = Mathf.Abs(_enemy.transform.position.x - _enemy.FieldOfView.Target.transform.position.x);

        if (_enemy.IsThereTargetInRangeOfDistance(_enemy.StrikeAttackDistance)) {

            _enemy.delayStrikeAttack -= Time.deltaTime;
            ChangeToStateAttackAfterDelay(_enemy, _enemy.StrikeState, _enemy.delayStrikeAttack);
        }
        else if (_enemy.IsThereTargetInRangeOfDistance(_enemy.AttackRangeDistance)) {

            _enemy.Config.delayAttack -= Time.deltaTime;
            ChangeToStateAttackAfterDelay(_enemy, _enemy.AttackState, _enemy.Config.delayAttack);
        }
        else {
            _enemy.Animator.Play(AnimationName.Run);
        }
    }

    public override void FixedUpdate() {
        MoveIfPlayRunAnimation(_enemy);
    }

    public override void Exit() {
        RefreshDelayForDifferentAttacks(_enemy);
    }
}
