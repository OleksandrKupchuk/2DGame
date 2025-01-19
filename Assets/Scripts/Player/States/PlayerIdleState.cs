using UnityEngine;

public class PlayerIdleState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        //Debug.Log($"<color=yellow>enter idle state</color>");

        _player = owner;
        _player.AnimationController.PlayAnimation(AnimationName.Idle);
    }

    public void Update() {
        //Debug.Log("info = " + _playerConfig.Animator.GetCurrentAnimatorStateInfo(0).IsName(PlayerAnimationName.Attack));
        //Debug.Log($"<color=yellow>idle execute</color>");
        //Debug.Log("jump button press = " + _playerConfig.Jump.action.triggered);
        if (!_player.PlayerMovement.IsGround()) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }
        else if (_player.PlayerMovement.IsJump) {
            _player.StateMachine.ChangeState(_player.JumpUpState);
        }
        else if (_player.PlayerMovement.IsAttack) {
            _player.StateMachine.ChangeState(_player.AttackState);
        }
        else if (_player.PlayerMovement.IsMove) {
            _player.StateMachine.ChangeState(_player.RunState);
        }

        if (_player.PlayerMovement.IsInteraction) {
            _player.Interactive?.Interact();
        }
    }

    public void FixedUpdate() {
    }

    public void Exit() {
        //Debug.Log($"<color=red>exit </color><color=yellow>idle state</color>");
    }
}
