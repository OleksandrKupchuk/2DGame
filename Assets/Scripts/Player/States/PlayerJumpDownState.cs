using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpDownState : IState<Player> {
    private Player _player;
    private float _gravityScale = 3.8f;

    public void Enter(Player owner) {
        Debug.Log($"<color=black>enter jumpDown state</color>");
        _player = owner;
        _player.Animator.Play(PlayerAnimationName.JumpDown);
        _player.SetGravityScale(_gravityScale);
    }

    public void ExecuteUpdate() {
        if (_player.isHit) {
            _player.isHit = false;
            _player.InvulnerableStatus.PlayBlinkAnimation();
        }
        if (_player.IsGround()) {
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
        _player.ResetRigidbodyVelocity();
        _player.ResetGravityScaleToDefault();
    }
}
