using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        _player = owner;
        _player.isAttack = false;
        _player.Animator.Play(PlayerAnimationName.Attack);
    }

    public void ExecuteUpdate() {
        if (_player.IsEndCurrentAnimation(AnimatorLayers.BaseLayer)) {
            _player.StateMachine.ChangeState(_player.IdleState);
            Debug.Log(_player.Animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).normalizedTime);
        }
    }

    public void ExecuteFixedUpdate() {

    }

    public void Exit() {
        
    }
}
