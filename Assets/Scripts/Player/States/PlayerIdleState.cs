using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        Debug.Log($"<color=yellow>enter idle state</color>");

        _player = owner;
        _player.ShotInputAction.action.performed += _player.SetAttackBoolTrue;
        _player.JumpInputAction.action.performed += _player.SetJumpBoolTrue;
        _player.Animator.Play(PlayerAnimationName.Idle);
    }

    public void ExecuteUpdate() {
        Debug.Log($"<color=yellow>idle execute</color>");

        if (_player.isJump) {
            _player.StateMachine.ChangeState(_player.JumpUpState);
        }
        if (_player.isAttack) {
            _player.StateMachine.ChangeState(_player.AttackState);
        }
        if (Mathf.Abs(_player.GetMovementInput().x) > 0) {
            _player.StateMachine.ChangeState(_player.RunState);
        }
    }

    public void ExecuteFixedUpdate() {
        Debug.Log($"<color=yellow>idle fixed execute</color>");
    }

    public void Exit() {
        Debug.Log($"<color=yellow>exit idle state</color>");

        _player.ShotInputAction.action.performed -= _player.SetAttackBoolTrue;
        _player.JumpInputAction.action.performed -= _player.SetJumpBoolTrue;
        _player.Animator.StopPlayback();
    }
}
