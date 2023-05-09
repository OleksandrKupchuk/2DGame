using UnityEngine;

public class EnemyDragonWarriorStrikeState : IState<Enemy> {

    private EnemyDragorWarrior _enemy;
    private float _speed = 7;
    private float _time;

    public void Enter(Enemy owner) {
        _enemy = (EnemyDragorWarrior)owner;
        _enemy.Animator.Play(AnimationName.Strike);
        _enemy.Rigidbody.gravityScale = 0;
        _time = 2f;
    }

    public void Update() {
    }

    public void FixedUpdate() {
        if (!_enemy.canMoveStrike) {
            return;
        }

        _time -= Time.deltaTime;
        _enemy.MoveEaseInQuint(_enemy.GetDirectionX, _speed, _time);

        if (_time <= 1) {
            _enemy.StateMachine.ChangeState(_enemy.IdleState);
            return;
        }
    }

    public void Exit() {
        _enemy.DisableStrikeCollider();
        _enemy.canMoveStrike = false;
    }
}
