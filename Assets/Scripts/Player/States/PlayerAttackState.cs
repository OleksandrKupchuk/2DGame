using UnityEngine;

public class PlayerAttackState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        _player = owner;
        _player.Animator.Play(PlayerAnimationName.Attack);
    }

    public void Update() {
        //Debug.Log("attack normalize time = " + _player.Animator.GetAnimatorTransitionInfo(AnimatorLayers.BaseLayer).normalizedTime);
        if (_player.IsEndCurrentAnimation(_player.Animator, AnimatorLayers.BaseLayer)) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }

    public void FixedUpdate() {

    }

    public void Exit() {
    }
}
