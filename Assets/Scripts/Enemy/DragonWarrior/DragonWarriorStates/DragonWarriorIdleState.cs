using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWarriorIdleState : IState<DragonWarrior>{
    private DragonWarrior _dragonWarrior;
    private float _timer;
    public void Enter(DragonWarrior owner) {
        Debug.Log($"<color=yellow>enter run state</color>");

        _dragonWarrior = owner;
        _dragonWarrior.Animator.Play(AnimationName.Idle);
        _timer = 4f;
        //_dragonWarrior.Flip();
    }

    public void ExecuteUpdate() {
        _timer -= Time.deltaTime;
        if(_timer <= 0) {
            _dragonWarrior.StateMachine.ChangeState(_dragonWarrior.AttckState);
        }
    }

    public void ExecuteFixedUpdate() {
    }

    public void Exit() {
        Debug.Log($"<color=red>exit</color> <color=yellow>run state</color>");
    }
}
