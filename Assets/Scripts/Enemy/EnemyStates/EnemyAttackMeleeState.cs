using UnityEngine;

public class EnemyAttackMeleeState : IState<BasicEnemy> {

    private BasicEnemy _enemy;
    public virtual void Enter(BasicEnemy owner) {
        _enemy = owner;
        _enemy.ResetRigidbodyVelocity();
        _enemy.delayAttack = 1f;
        _enemy.Animator.Play(AnimationName.AttackMelee);
        Debug.Log("delay attack = " + _enemy.delayAttack);
    }

    public virtual void ExecuteUpdate() {
        if (_enemy.IsEndCurrentAnimation(_enemy.Animator, AnimatorLayers.BaseLayer)) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public virtual void ExecuteFixedUpdate() {
    }

    public virtual void Exit() {
    }
}
