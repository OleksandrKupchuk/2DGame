using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        // Debug.Log($"<color=white>enter hit state</color>");
        _player = owner;
        _player.Animator.Play(PlayerAnimationName.Hit);
        //_player.InvulnerableStatus.StartBlinkAnimation(_player.Animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).length);
        _player.InvulnerableStatus.StartBlinkGameObhectsAnimation();
    }

    public void ExecuteUpdate() {
        if (_player.IsEndCurrentAnimation(AnimatorLayers.BaseLayer)) {
            // Debug.Log("change on Idle");
            // Debug.Log("hit = " + _player.Animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).normalizedTime);
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }

    public void ExecuteFixedUpdate() {
    }

    public void Exit() {
        _player.isHit = false;
        // Debug.Log($"<color=red>exit </color><color=white>hit state</color>");
    }
}
