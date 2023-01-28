using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonWarriorAttack : IState<DragonWarrior> {
    private DragonWarrior _dragonWarrior;

    public void Enter(DragonWarrior owner) {
        _dragonWarrior = owner;
        _dragonWarrior.Animator.Play(AnimationName.Attack);
        _dragonWarrior.EnableFireBall(_dragonWarrior.FireBallsPrefabs, _dragonWarrior.ShotPoint, _dragonWarrior.transform.localScale.x);
    }

    public void ExecuteUpdate() {
        if (_dragonWarrior.IsEndCurrentAnimation(_dragonWarrior.Animator, AnimatorLayers.BaseLayer)) {
            _dragonWarrior.StateMachine.ChangeState(_dragonWarrior.IdleState);
        }
    }

    public void ExecuteFixedUpdate() {
    }

    public void Exit() {
    }
}
