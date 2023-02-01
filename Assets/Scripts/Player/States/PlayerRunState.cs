using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        //Debug.Log($"<color=blue>enter run state</color>");

        _player = owner;
        _player.Animator.Play(AnimationName.Run);
    }

    public void ExecuteUpdate() {
        //Debug.Log($"<color=blue>run execute</color>");
        //Debug.Log("jump button = " + _player.JumpInputAction.action.ReadValue<bool>());

        if (_player.isHit) {
            _player.StateMachine.ChangeState(_player.HitState);
        }
        else if (!_player.IsGround()) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }
        else if (_player.CanJump) {
            _player.StateMachine.ChangeState(_player.JumpUpState);
        }
        else if (_player.IsAttack) {
            _player.StateMachine.ChangeState(_player.AttackState);
        }
        else if (Mathf.Abs(_player.GetMovementInput().x) == 0) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }

        _player.Flip();
    }

    public void ExecuteFixedUpdate() {
        //Debug.Log($"<color=blue>run fixed execute</color>");

        if (_player.IsAttack) {
            return;
        }

        _player.Move(_player.MovementInput.x);
    }

    public void Exit() {
        //Debug.Log($"<color=red>exit</color> <color=blue>run state</color>");
        //Debug.Log("exit run = " + _player.Rigidbody.velocity);

        _player.ResetRigidbodyVelocity();
        _player.Animator.StopPlayback();
    }
}
