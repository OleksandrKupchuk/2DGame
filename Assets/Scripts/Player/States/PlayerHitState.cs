public class PlayerHitState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        //Debug.Log($"<color=white>enter hit state</color>");
        _player = owner;
        _player.AnimationController.PlayAnimation(AnimationName.Hit);
    }

    public void Update() {
        if (_player.AnimationController.IsEndCurrentAnimation(AnimatorLayers.BaseLayer, AnimationName.Hit)) {
            _player.StateMachine.ChangeState(_player.IdleState);
        }
    }

    public void FixedUpdate() { }

    public void Exit() {
        // Debug.Log($"<color=red>exit </color><color=white>hit state</color>");
    }
}