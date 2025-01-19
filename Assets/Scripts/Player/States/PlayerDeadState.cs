public class PlayerDeadState : IState<Player> {
    public void Enter(Player owner) {
        owner.AnimationController.PlayAnimation(AnimationName.Dead);
    }

    public void Update() {
    }

    public void FixedUpdate() {
    }

    public void Exit() {
    }
}
