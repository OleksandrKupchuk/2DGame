using UnityEngine;

public class EnemyKnightStrikeState : IState<Enemy> {

    private EnemyKnight _enemy;

    public void Enter(Enemy owner) {
        _enemy = (EnemyKnight)owner;
        _enemy.Animator.Play(AnimationName.Strike);
        _enemy.Rigidbody.gravityScale = 0;
        _enemy.delayStrikeAttack = 2f;
        //Debug.Log("strike collider = " + _enemy.StrikeCollider);
    }

    public void Update() {
    }

    public void FixedUpdate() {
        if(_enemy.IsEndCurrentAnimation(_enemy.Animator, AnimatorLayers.BaseLayer)) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public void Exit() {
        _enemy.DisableStrikeCollider();
    }
}
