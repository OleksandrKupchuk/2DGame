using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWarriorRunState : IState<DragonWarrior> {
    private DragonWarrior _dragonWarrior;
    private float _timer;

    public void Enter(DragonWarrior owner) {
        Debug.Log($"<color=blue>enter run state</color>");

        _dragonWarrior = owner;
        _dragonWarrior.Animator.Play(AnimationName.Run);
        _timer = 4f;
    }

    public void ExecuteUpdate() {

        _timer -= Time.deltaTime;
        if (_timer <= 0) {
            _dragonWarrior.StateMachine.ChangeState(_dragonWarrior.IdleState);
        }
    }

    public void ExecuteFixedUpdate() {
        _dragonWarrior.Move();
    }

    public void Exit() {
        Debug.Log($"<color=red>exit</color> <color=blue>run state</color>");
        _dragonWarrior.ResetRigidbodyVelocity();
    }
}
