using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        Debug.Log($"<color=blue>enter run state</color>");

        _player = owner;
        _player.Animator.Play(PlayerAnimationName.Run);
        _player.ShotInputAction.action.performed += _player.SetAttackBoolTrue;
        _player.JumpInputAction.action.performed += _player.SetJumpBoolTrue;
    }

    public void ExecuteUpdate() {
        //Debug.Log($"<color=blue>run execute</color>");

        if (_player.isHit) {
            _player.StateMachine.ChangeState(_player.HitState);
        }
        else if (_player.isJump) {
            Debug.Log("jump");
            _player.StateMachine.ChangeState(_player.JumpUpState);
        }
        else if (_player.isAttack) {
            _player.StateMachine.ChangeState(_player.AttackState);
        }
        else if (Mathf.Abs(_player.GetMovementInput().x) == 0) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }

        _player.Flip();
    }

    public void ExecuteFixedUpdate() {
        //Debug.Log($"<color=blue>run fixed execute</color>");

        if (_player.isAttack) {
            return;
        }

        _player.Move();
    }

    public void Exit() {
        Debug.Log($"<color=red>exit</color> <color=blue>run state</color>");
        //Debug.Log("exit run = " + _player.Rigidbody.velocity);

        _player.ResetRigidbodyVelocity();
        _player.Animator.StopPlayback();
        _player.ShotInputAction.action.performed -= _player.SetAttackBoolTrue;
        _player.JumpInputAction.action.performed -= _player.SetJumpBoolTrue;
    }
}
