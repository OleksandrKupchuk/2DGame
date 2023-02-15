using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttackState : IState<EnemyOfMelee> {

    private EnemyOfMelee _enemy;
    public virtual void Enter(EnemyOfMelee owner) {
        _enemy = owner;
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
