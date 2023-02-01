using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : IState<EnemyOfMelee> {
    protected EnemyOfMelee _enemy;
    private float _timer;

    public virtual void Enter(EnemyOfMelee owner) {
        _enemy = owner;
        _enemy.Animator.Play(AnimationName.Idle);
        _timer = 3f;
        Debug.Log("EnemyIdle state enter");
        _enemy.Flip();
    }

    public virtual void ExecuteUpdate() {
        _timer -= Time.deltaTime;
        if(_timer <= 0) {
            _enemy.StateMachine.ChangeState(_enemy.RunState);
        }
    }

    public virtual void ExecuteFixedUpdate() {
    }

    public virtual void Exit() {
    }
}
