using UnityEngine;

public class EnemyKnightDetectTargetState : EnemyDetectTargetState {

    private EnemyKnight _enemy;
    private int _chanceStrikeAttack = 40;
    private int _randomChance;

    public override void Enter(BasicEnemy owner) {
        _enemy = (EnemyKnight)owner;
        _randomChance = Random.Range(0, 100);
    }

    public override void Update() {

        CheckHasTargetAndChangeToIdleState(_enemy);

        CheckNeedFlipAndFlip(_enemy);

        CalculationDistanceToTarget(_enemy);

        if (_enemy.IsThereTargetInRangeOfDistance(_enemy.AttackMeleeDistance)) {
            CalculationAttackStateOrStrikeAttackState();
        }
        else {
            _enemy.Animator.Play(AnimationName.Run);
        }
    }

    private void CalculationAttackStateOrStrikeAttackState() {
        if(_randomChance <= _chanceStrikeAttack && _enemy.IsLeftHalfOfHealth) {
            _enemy.ConfigBuffer.delayStrikeAttack -= Time.deltaTime;
            ChangeToStateAttackAfterDelay(_enemy, _enemy.StrikeState, _enemy.ConfigBuffer.delayStrikeAttack);
        }
        else {
            _enemy.ConfigBuffer.delayAttack -= Time.deltaTime;
            ChangeToStateAttackAfterDelay(_enemy, _enemy.AttackState, _enemy.ConfigBuffer.delayAttack);
        }
    }

    public override void FixedUpdate() {
        MoveIfPlayRunAnimation(_enemy);
    }

    public override void Exit() {
        RefreshDelayForDifferentAttacks(_enemy);
    }
}