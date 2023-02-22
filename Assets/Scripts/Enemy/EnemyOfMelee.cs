using UnityEngine;

public class EnemyOfMelee : BasicEnemy {

    protected AnimationEvent _enableAttackColliderEvent = new AnimationEvent();

    [SerializeField]
    protected EdgeCollider2D _attackCollider;
    [SerializeField]
    protected int _frameRateInAttackAnimationForEnableAttackCollider;
    [SerializeField]
    protected AnimationClip _attackAnimation;

    private new void Awake() {
        base.Awake();
        AttackDistance = 3.2f;
        delayAttack = 0f;
        DisableAttackCollider();
    }

    protected void Start() {
        StateMachine.ChangeState(IdleState);
    }

    protected void Update() {
        StateMachine.Update();
        FieldOfView.SetStartPoint(transform.position);
    }

    protected void FixedUpdate() {
        StateMachine.FixedUpdate();
    }

    public void AddEnableAttackCoolliderEventForAttackAnimation() {
        LogicEnemy.AddEventForFrameOfAnimation(_attackAnimation, _enableAttackColliderEvent, _frameRateInAttackAnimationForEnableAttackCollider, nameof(EnableAttackCollider));
    }

    public void AddDisableAttackCoolliderEventForAttackAnimation() {
        LogicEnemy.AddEventToEndOfAnimation(_attackAnimation, _enableAttackColliderEvent, nameof(DisableAttackCollider));
    }

    public void DisableAttackCollider() {
        LogicEnemy.DisableCollider(_attackCollider);
    }

    public void EnableAttackCollider() {
        LogicEnemy.EnableCollider(_attackCollider);
    }
}
