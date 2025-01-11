using UnityEngine;

public class PlayerJumpDownState : IState<Player> {
    private Player _player;
    //private float _gravityScale = 3.8f;

    public void Enter(Player owner) {
        //Debug.Log($"<color=black>enter jumpDown state</color>");
        _player = owner;
        _player.Animator.Play(PlayerAnimationName.JumpDown);
        //_playerConfig.SetGravityScale(_gravityScale);
    }

    public void Update() {
        if (_player.PlayerMovement.IsGround()) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }

        _player.PlayerMovement.GetMoveInput();
        _player.PlayerMovement.Flip();
    }

    public void FixedUpdate() {
        if(_player.PlayerMovement.GetMoveInput() == Vector2.zero) {
            return;
        }
        _player.PlayerMovement.Run(_player.PlayerMovement.GetMoveInput().x);
    }

    public void Exit() {
        //Debug.Log($"<color=red>exit</color> <color=black>jumpDown state</color>");
        _player.ResetRigidbodyVelocity();
        _player.ResetGravityScaleToDefault();
    }
}
