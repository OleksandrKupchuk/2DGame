using UnityEngine;

public class PlayerJumpUpState : IState<Player> {
    private Player _player;
    private float _timerToFalling;

    public void Enter(Player owner) {
        //Debug.Log($"<color=green>enter jumpUp state</color>");
        _player = owner;
        _player.AnimationController.PlayAnimation(PlayerAnimationName.JumpUp);  
        _player.PlayerMovement.Jump();
        _timerToFalling = 0.5f;
        //_timerToFalling = 0f;
    }

    public void Update() {
        //Debug.Log("is falling = " + _playerConfig.IsFalling);
        //Debug.Log("jump button press = " + _playerConfig.Jump.action.triggered);
        _timerToFalling -= Time.deltaTime;

        if (_player.PlayerMovement.IsGround() && _timerToFalling <= 0) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
        else if (_player.PlayerMovement.IsFalling) {
            _player.StateMachine.ChangeState(_player.JumpDownState);
        }

        _player.PlayerMovement.Flip();
    }

    public void FixedUpdate() {
        if (!_player.PlayerMovement.IsMove) {
            return;
        }

        _player.PlayerMovement.Run();
    }

    public void Exit() {
        //Debug.Log($"<color=red>exit</color> <color=green>jumpUp state</color>");
    }
}