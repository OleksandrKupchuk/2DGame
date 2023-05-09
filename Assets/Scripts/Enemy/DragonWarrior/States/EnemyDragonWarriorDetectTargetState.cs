using UnityEngine;

public class EnemyDragonWarriorDetectTargetState : EnemyDetectTargetState {

    private EnemyDragorWarrior _enemy;
    private int _chanceStrikeAttack = 30;
    private int _randomChance;

    private bool CanStrikeAttack { get => _enemy.IsThereTargetInRangeOfDistance(_enemy.Config.distanceStrikeAttack) && _randomChance <= _chanceStrikeAttack; }

    public override void Enter(Enemy owner) {
        _enemy = (EnemyDragorWarrior)owner;
        _randomChance = Random.Range(0, 100);
    }

    public override void Update() {
        _enemyDetectLogic.CheckHasTargetAndChangeToIdleState(_enemy);

        _enemyDetectLogic.CheckNeedFlipAndFlip(_enemy);

        _enemyDetectLogic.CalculationDistanceToTarget(_enemy);

        if (CanStrikeAttack) {

            _enemy.delayStrikeAttack -= Time.deltaTime;
            _enemyDetectLogic.ChangeToStateAttackAfterDelay(_enemy, _enemy.StrikeState, _enemy.delayStrikeAttack);
        }
        else if (_enemy.IsThereTargetInRangeOfDistance(_enemy.Config.distanceRangeAttack)) {

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