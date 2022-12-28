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
    }

    public void ExecuteUpdate() {
        Debug.Log($"<color=blue>run execute</color>");

        if (_player.isAttack) {
            _player.StateMachine.ChangeState(_player.AttackState);
        }
        if (Mathf.Abs(_player.GetMovementInput().x) == 0) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }

        _player.Flip();
    }

    public void ExecuteFixedUpdate() {
        Debug.Log($"<color=blue>run fixed execute</color>");

        if (_player.isAttack) {
            return;
        }

        _player.Move();
    }

    public void Exit() {
        Debug.Log($"<color=blue>exit run state</color>");
        Debug.Log("exit run = " + _player.Rigidbody.velocity);

        _player.ResetRigidbodyVelocity();
        _player.ShotInputAction.action.performed -= _player.SetAttackBoolTrue;
        _player.Animator.StopPlayback();
    }
}
