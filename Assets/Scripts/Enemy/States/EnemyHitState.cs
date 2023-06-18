using UnityEngine;

public class EnemyHitState : IState<Enemy> {

    protected Enemy _enemy;

    public virtual void Enter(Enemy owner) {
        _enemy = owner;
        _enemy.Animator.Play(EnemyAnimationName.Hit);
        Debug.Log("enemy hit state");
    }

    public virtual void Update() { }

    public virtual void FixedUpdate() { }

    public virtual void Exit() {}
}