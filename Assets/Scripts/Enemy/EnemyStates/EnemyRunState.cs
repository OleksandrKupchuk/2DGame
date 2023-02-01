using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunState : IState<EnemyOfMelee> {
    protected EnemyOfMelee _enemy;
    private float _timer;

    public virtual void Enter(EnemyOfMelee owner) {
        _enemy = owner;
        _enemy.Animator.Play(AnimationName.Run);
        _timer = 4f;
        Debug.Log("EnemyRunState enter");
    }

    public virtual void ExecuteFixedUpdate() {
        _timer -= Time.deltaTime;
        if(_timer <= 0) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public virtual void ExecuteUpdate() {
        _enemy.Move(_enemy.GetLocalScaleX);
    }

    public virtual void Exit() {
        _enemy.ResetRigidbodyVelocity();
    }
}
