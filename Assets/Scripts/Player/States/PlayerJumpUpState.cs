using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpUpState : IState<Player> {
    private Player _player;
    private float _timer;

    public void Enter(Player owner) {
        Debug.Log($"<color=green>enter jumpUp state</color>");
        _player = owner;
        _player.Animator.Play(AnimationName.JumpUp);
        _player.Jump();
        _timer = 0.5f;
    }

    public void ExecuteUpdate() {
        //Debug.Log("is falling = " + _player.IsFalling);
        Debug.Log("is ground = " + _player.IsGround());
        //Debug.Log("jump button press = " + _player.JumpInputAction.action.triggered);
        _timer -= Time.deltaTime;

        if (_player.isHit) {
            _player.isHit = false;
            _player.InvulnerableStatus.PlayBlinkAnimation();
        }
        if (_player.IsGround() && _timer <= 0) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
        else if (_player.IsFalling) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }

        _player.GetMovementInput();
        _player.Flip();
    }

    public void ExecuteFixedUpdate() {

        if (_player.GetMovementInput() == Vector2.zero) {
            return;
        }
        _player.Move(_player.GetMovementInput().x);
    }

    public void Exit() {
        Debug.Log($"<color=red>exit</color> <color=green>jumpUp state</color>");
        //_player.Animator.StopPlayback();
    }
}
