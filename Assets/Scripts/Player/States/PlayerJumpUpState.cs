using UnityEngine;

public class PlayerJumpUpState : IState<Player> {
    private Player _player;
    private float _timer;

    public void Enter(Player owner) {
        //Debug.Log($"<color=green>enter jumpUp state</color>");
        _player = owner;
        _player.Animator.Play(PlayerAnimationName.JumpUp);
        _player.PlayerMovement.Jump();
        _timer = 0.5f;
    }

    public void Update() {
        //Debug.Log("is falling = " + _playerConfig.IsFalling);
        //Debug.Log("jump button press = " + _playerConfig.Jump.action.triggered);
        _timer -= Time.deltaTime;

        if (_player.PlayerMovement.IsGround() && _timer <= 0) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
        else if (_player.PlayerMovement.IsFalling) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }

        _player.PlayerMovement.GetMoveInput();
        _player.PlayerMovement.Flip();
    }

    public void FixedUpdate() {

        if (_player.PlayerMovement.GetMoveInput() == Vector2.zero) {
            return;
        }
        _player.PlayerMovement.Run(_player.PlayerMovement.GetMoveInput().x);
    }

    public void Exit() {
        //Debug.Log($"<color=red>exit</color> <color=green>jumpUp state</color>");
    }
}