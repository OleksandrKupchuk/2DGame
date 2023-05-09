using UnityEngine;

public class EnemyKnightDetectTargetState : EnemyDetectTargetState {

    private EnemyKnight _enemy;
    private int _chanceStrikeAttack = 40;
    private int _randomChance;

    private bool CanStrikeAttack { get => _randomChance <= _chanceStrikeAttack && _enemy.IsLeftHalfOfHealth; }

    public override void Enter(Enemy owner) {
        _enemy = (EnemyKnight)owner;
        _randomChance = Random.Range(0, 100);
    }

    public override void Update() {

        _enemyDetectLogic.CheckHasTargetAndChangeToIdleState(_enemy);

        _enemyDetectLogic.CheckNeedFlipAndFlip(_enemy);

        _enemyDetectLogic.CalculationDistanceToTarget(_enemy);

        if (_enemy.IsThereTargetInRangeOfDistance(_enemy.Config.distanceMeleeAttack)) {
            CalculationAttackStateOrStrikeAttackState();
        }
        else {
            _enemy.Animator.Play(AnimationName.Run);
        }
    }

    private void CalculationAttackStateOrStrikeAttackState() {
        if(CanStrikeAttack) {
            _enemy.delayStrikeAttack -= Time.deltaTime;
            _enemyDetectLogic.ChangeToStateAttackAfterDelay(_enemy, _enemy.StrikeState, _enemy.delayStrikeAttack);
        }
        else {
            _enemy.delayAttack -= Time.deltaTime;
            _enemyDetectLogic.ChangeToStateAttackAfterDelay(_enemy, _enemy.AttackState, _enemy.delayAttack);
        }
    }

    public override void FixedUpdate() {
        _enemyDetectLogic.MoveIfPlayRunAnimation(_enemy);
    }

    public override void Exit() {
        _enemyDetectLogic.RefreshDelayForDifferentAttacks(_enemy);
    }
}