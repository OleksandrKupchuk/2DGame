using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpUpState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        _player = owner;
        _player.isJump = false;
        _player.Jump();
        _player.Animator.Play(PlayerAnimationName.JumpUp);
    }

    public void ExecuteUpdate() {
        if (_player.CheckGround.IsGround) {
            Debug.Log(_player.Animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).normalizedTime);
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }

    public void ExecuteFixedUpdate() {
        
    }

    public void Exit() {
       
    }
}
