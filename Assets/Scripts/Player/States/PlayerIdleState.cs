using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        Debug.Log($"<color=yellow>enter idle state</color>");

        _player = owner;
        _player.ShotInputAction.action.performed += _player.PlayerAttack;
        _player.JumpInputAction.action.performed += _player.SetJumpBoolTrue;
        //_player.Animator.Play(PlayerAnimationName.Idle);
    }

    public void ExecuteUpdate() {
        //Debug.Log("info = " + _player.Animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationName.Attack));
        //Debug.Log($"<color=yellow>idle execute</color>");
        if (_player.isHit) {
            _player.StateMachine.ChangeState(_player.HitState);
        }
        else if (_player.isJump) {
            _player.StateMachine.ChangeState(_player.JumpUpState);
        }
        //else if (_player.isAttack) {
        //    _player.StateMachine.ChangeState(_player.AttackState);
        //}
        else if (Mathf.Abs(_player.GetMovementInput().x) > 0) {
            _player.StateMachine.ChangeState(_player.RunState);
        }
    }

    public void ExecuteFixedUpdate() {
        //Debug.Log($"<color=yellow>idle fixed execute</color>");
    }

    public void Exit() {
        Debug.Log($"<color=red>exit </color><color=yellow>idle state</color>");

        _player.ShotInputAction.action.performed -= _player.PlayerAttack;
        //_player.ShotInputAction.action.performed -= _player.PlayerAttack;
    }
}
