using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        // Debug.Log($"<color=white>enter hit state</color>");
        _player = owner;
        _player.Animator.Play(AnimationName.Hit);
        _player.InvulnerableStatus.PlayBlinkAnimation();
    }

    public void Update() {
        if (_player.IsEndCurrentAnimation(_player.Animator, AnimatorLayers.BaseLayer)) {
            // Debug.Log("change on Idle");
            // Debug.Log("hit = " + _player.Animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).normalizedTime);
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }

    public void FixedUpdate() {
    }

    public void Exit() {
        _player.isHit = false;
        // Debug.Log($"<color=red>exit </color><color=white>hit state</color>");
    }
}
