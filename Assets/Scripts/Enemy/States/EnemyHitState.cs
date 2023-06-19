using UnityEngine;

public class EnemyHitState : IState<Enemy> {
    protected Enemy _enemy;

    public virtual void Enter(Enemy owner) {
        _enemy = owner;
        _enemy.Animator.Play(AnimationName.Hit);
        Debug.Log("enemy hit state");
    }

    public virtual void Update() { 
        if(_enemy.IsEndCurrentAnimation(_enemy.Animator, AnimatorLayers.BaseLayer, AnimationName.Hit)) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
        }
    }

    public virtual void FixedUpdate() { }

    public virtual void Exit() {}
}