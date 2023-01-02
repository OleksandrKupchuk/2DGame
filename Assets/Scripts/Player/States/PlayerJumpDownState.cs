using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDownState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        Debug.Log($"<color=black>enter jumpDown state</color>");
        _player = owner;
        _player.Animator.Play(PlayerAnimationName.JumpDown);
    }

    public void ExecuteUpdate() {
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
        Debug.Log($"<color=red>exit</color> <color=black>jumpDown state</color>");
        _player.Animator.StopPlayback();
    }
}
