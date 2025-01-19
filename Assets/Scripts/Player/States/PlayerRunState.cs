using UnityEngine;

public class PlayerRunState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        //Debug.Log($"<color=blue>enter run state</color>");

        _player = owner;
        _player.AnimationController.PlayAnimation(AnimationName.Run);
    }

    public void Update() {
        //Debug.Log($"<color=blue>run execute</color>");
        //Debug.Log("jump button = " + _playerConfig.Jump.action.ReadValue<bool>());
        if (!_player.PlayerMovement.IsGround()) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }
        else if (_player.PlayerMovement.IsJump) {
            _player.StateMachine.ChangeState(_player.JumpUpState);
        }
        else if (_player.PlayerMovement.IsAttack) {
            _player.StateMachine.ChangeState(_player.AttackState);
        }
        else if (!_player.PlayerMovement.IsMove) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }

        _player.PlayerMovement.Flip();
    }

    public void FixedUpdate() {
        //Debug.Log($"<color=blue>run fixed execute</color>");

        if (_player.PlayerMovement.IsAttack) {
            return;
        }

        _player.PlayerMovement.Run();
    }

    public void Exit() {
        //Debug.Log($"<color=red>exit</color> <color=blue>run state</color>");
        //Debug.Log("exit run = " + _playerConfig.Rigidbody.velocity);

        _player.PlayerMovement.ResetRigidbodyVelocity();
    }
}
