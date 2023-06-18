using UnityEngine;

public class PlayerHitState : IState<Player> {
    private Player _player;

    public void Enter(Player owner) {
        //Debug.Log($"<color=white>enter hit state</color>");
        _player = owner;
        _player.Animator.Play(AnimationName.Hit);
        _player.InvulnerableStatus.PlayBlinkAnimation();
    }

    public void Update() {}

    public void FixedUpdate() {}

    public void Exit() {
        // Debug.Log($"<color=red>exit </color><color=white>hit state</color>");
    }
}