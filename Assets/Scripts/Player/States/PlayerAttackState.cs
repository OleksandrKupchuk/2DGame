using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : IState<Player> {
    private Player _player;
    private int _baseLayer = 0;
    private bool IsEndAttackAnimation {
        get {
            if (_player.Animator.GetCurrentAnimatorStateInfo(_baseLayer).normalizedTime >= 1) {
                return true;
            }

            return false;
        }
    }

    public void Enter(Player owner) {
        _player = owner;
        _player.isAttack = false;
        _player.Animator.Play(PlayerAnimationName.Attack);
    }

    public void ExecuteUpdate() {
        if (IsEndAttackAnimation) {
            _player.StateMachine.ChangeState(_player.IdleState);
            Debug.Log(_player.Animator.GetCurrentAnimatorStateInfo(_baseLayer).normalizedTime);
        }
    }

    public void ExecuteFixedUpdate() {

    }

    public void Exit() {
        
    }
}
