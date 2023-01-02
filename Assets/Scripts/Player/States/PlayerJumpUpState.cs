using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpUpState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        Debug.Log($"<color=green>enter jumpUp state</color>");
        _player = owner;
        _player.isJump = false;
        _player.Jump();
        _player.Animator.Play(PlayerAnimationName.JumpUp);
    }

    public void ExecuteUpdate() {
        
        if (_player.IsFalling) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }
        if (_player.CheckGround.IsGround) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }

        _player.GetMovementInput();
        _player.Flip();
    }

    public void ExecuteFixedUpdate() {
        if(_player.GetMovementInput() == Vector2.zero) {
            return;
        }
        _player.Move();
    }

    public void Exit() {
        Debug.Log($"<color=red>exit</color> <color=green>jumpUp state</color>");
        _player.Animator.StopPlayback();
    }
}
