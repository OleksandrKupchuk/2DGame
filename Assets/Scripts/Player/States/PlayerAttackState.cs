using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        _player = owner;
        _player.Animator.Play(PlayerAnimationName.Attack);
    }

    public void ExecuteUpdate() {
        if (_player.isHit) {
            _player.StateMachine.ChangeState(_player.HitState);
        }
        else if (_player.IsEndCurrentAnimation(AnimatorLayers.BaseLayer)) {
            Debug.Log("next IdleState");
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }

    public void ExecuteFixedUpdate() {

    }

    public void Exit() {
    }
}
