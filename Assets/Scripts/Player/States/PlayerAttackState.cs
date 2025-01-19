public class PlayerAttackState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        _player = owner;
        _player.AnimationController.PlayAnimation(PlayerAnimationName.Attack);
    }

    public void Update() {
        if (_player.AnimationController.IsEndCurrentAnimation(AnimatorLayers.BaseLayer)) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }

    public void FixedUpdate() {

    }

    public void Exit() {
    }
}
