using UnityEngine;

public class EnemyDragonWarriorStrikeState : IState<BasicEnemy> {

    private EnemyDragorWarrior _enemy;
    private float _speed = 7;
    private float _time;

    public void Enter(BasicEnemy owner) {
        _enemy = (EnemyDragorWarrior)owner;
        _enemy.Animator.Play(AnimationName.Strike);
        _enemy.Rigidbody.gravityScale = 0;
        _time = 2f;
    }

    public void Update() {
    }

    public void FixedUpdate() {

        _time -= Time.deltaTime;
        _enemy.MoveEaseInQuint(_enemy.GetDirectionX, _speed, _time);

        if (_time <= 1) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
            return;
        }
    }

    public void Exit() {
        _enemy.DisableStrikeCollider();
    }
}
