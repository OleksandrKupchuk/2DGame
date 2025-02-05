using UnityEngine;

public class PlayerJumpUpState : IState<Player> {
    private Player _player;
    private float _timer;

    public void Enter(Player owner) {
        //Debug.Log($"<color=green>enter jumpUp state</color>");
        _player = owner;
        _player.Animator.Play(PlayerAnimationName.JumpUp);
        _player.Jump();
        _timer = 0.5f;
    }

    public void Update() {
        //Debug.Log("is falling = " + _player.IsFalling);
        //Debug.Log("jump button press = " + _player.JumpInputAction.action.triggered);
        _timer -= Time.deltaTime;

        if (_player.IsGround() && _timer <= 0) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
        else if (_player.IsFalling) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }

        _player.GetMovementInput();
        _player.Flip();
    }

    public void FixedUpdate() {

        if (_player.GetMovementInput() == Vector2.zero) {
            return;
        }
        _player.Move(_player.GetMovementInput().x);
    }

    public void Exit() {
        //Debug.Log($"<color=red>exit</color> <color=green>jumpUp state</color>");
    }
}