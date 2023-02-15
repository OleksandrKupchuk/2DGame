using UnityEngine;

public class EnemyIdleState : IState<EnemyOfMelee> {
    protected EnemyOfMelee _enemy;
    private float _timer;

    public virtual void Enter(EnemyOfMelee owner) {
        _enemy = owner;
        _enemy.Animator.Play(AnimationName.Idle);
        _timer = 3f;
        _enemy.delayAttack = 1.5f;
        //Debug.Log("EnemyIdle state enter");
    }

    public virtual void ExecuteUpdate() {
        _timer -= Time.deltaTime;
        //if(_timer <= 0) {
        //    _enemy.StateMachine.ChangeState(_enemy.RunState);
        //}
        if (_enemy.FieldOfView.Target != null) {
            if (_enemy.IsNeedLookOnPlayer()) {
                _enemy.Flip();
            }
            _enemy.delayAttack -= Time.deltaTime;
        }
        if (_enemy.delayAttack <= 0) {
            _enemy.StateMachine.ChangeState(_enemy.AttckState);
        }
    }

    public virtual void ExecuteFixedUpdate() {
    }

    public virtual void Exit() {
    }
}
