using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        Debug.Log($"<color=green>enter attack state</color>");
        _player = owner;
        _player.ResetRigidbodyVelocity();
        _player.Animator.SetTrigger(AnimatorParameters.Attack);
        //_player.Animator.Play(PlayerAnimationName.Attack);
    }

    public void ExecuteUpdate() {
        if (_player.isHit) {
            _player.StateMachine.ChangeState(_player.HitState);
        }
        else if (_player.IsEndCurrentAnimation(AnimatorLayers.BaseLayer)) {
            Debug.Log("next IdleState");
            _player.StateMachine.ChangeState(_player.IdleState);
        }
        Debug.Log("velocity attack = " + _player.Rigidbody.velocity);
    }

    public void ExecuteFixedUpdate() {

    }

    public void Exit() {
        _player.isAttack = false;
        Debug.Log($"<color=red>exit </color><color=green>attack state</color>");
    }
}
