using UnityEngine;

public class PlayerRunState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        //Debug.Log($"<color=blue>enter run state</color>");

        _player = owner;
        _player.Animator.Play(AnimationName.Run);
    }

    public void Update() {
        //Debug.Log($"<color=blue>run execute</color>");
        //Debug.Log("jump button = " + _player.JumpInputAction.action.ReadValue<bool>());
        if (!_player.PlayerMovement.IsGround()) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }
        else if (_player.PlayerMovement.IsJump) {
            _player.StateMachine.ChangeState(_player.JumpUpState);
        }
        else if (_player.PlayerMovement.IsAttack) {
            _player.StateMachine.ChangeState(_player.AttackState);
        }
        else if (Mathf.Abs(_player.PlayerMovement.GetMoveInput().x) == 0) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }

        _player.PlayerMovement.Flip();
    }

    public void FixedUpdate() {
        //Debug.Log($"<color=blue>run fixed execute</color>");

        //if (_player.Movement.IsAttack) {
        //    return;
        //}

        //_player.Move(_player.GetMovementInput().x, _player.PlayerAttributes.Speed);
        _player.PlayerMovement.Run(_player.PlayerMovement.GetMoveInput().x);
    }

    public void Exit() {
        //Debug.Log($"<color=red>exit</color> <color=blue>run state</color>");
        //Debug.Log("exit run = " + _player.Rigidbody.velocity);

        _player.ResetRigidbodyVelocity();
        _player.Animator.StopPlayback();
    }
}
