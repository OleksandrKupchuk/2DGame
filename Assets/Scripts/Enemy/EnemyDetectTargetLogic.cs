using UnityEngine;

public class EnemyDetectTargetLogic {
    public void CheckHasTargetAndChangeToIdleState(BasicEnemy enemy) {
        if (!enemy.HasTarget) {
            enemy.StateMachine.ChangeState(enemy.IdleState);
            return;
        }
    }

    public void CheckNeedFlipAndFlip(BasicEnemy enemy) {
        if (enemy.IsNeedLookOnPlayer()) {
            enemy.Flip();
        }
    }

    public void CalculationDistanceToTarget(BasicEnemy enemy) {
        if (enemy.FieldOfView.Target != null) {
            enemy.distanceToTarget = Mathf.Abs(enemy.transform.position.x - enemy.FieldOfView.Target.transform.position.x);
        }
    }

    public void CheckIsThereTargetInRangeOfAttackAndAttackOrRun(float distance, BasicEnemy enemy, EnemyAttackState attackState) {

        if (enemy.IsThereTargetInRangeOfDistance(distance)) {
            enemy.ResetRigidbodyVelocity();
            enemy.Animator.Play(AnimationName.Idle);

            HandleAttackDelay(enemy, attackState);
        }
        else {
            enemy.Animator.Play(AnimationName.Run);
        }
    }

    private void HandleAttackDelay(BasicEnemy enemy, EnemyAttackState attackState) {

        enemy.Config.delayAttack -= Time.deltaTime;

        if (enemy.Config.delayAttack <= 0) {
            enemy.StateMachine.ChangeState(attackState);
        }
    }

    public void ChangeToStateAttackAfterDelay(BasicEnemy enemy, IState<BasicEnemy> attackState, float delayAttack) {

        enemy.ResetRigidbodyVelocity();
        enemy.Animator.Play(AnimationName.Idle);

        if (delayAttack <= 0) {
            enemy.StateMachine.ChangeState(attackState);
            return;
        }
    }

    public void MoveIfPlayRunAnimation(BasicEnemy enemy) {
        if (enemy.Animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).IsName(AnimationName.Run)) {
            enemy.Move(enemy.GetDirectionX);
        }
    }

    public void RefreshDelayForDifferentAttacks(BasicEnemy enemy) {
        enemy.delayAttack = enemy.Config.delayAttack;
        enemy.delayStrikeAttack = enemy.Config.delayStrikeAttack;
    }
}
