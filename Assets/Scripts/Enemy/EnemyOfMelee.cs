using UnityEngine;

public class EnemyOfMelee : Enemy {

    protected AnimationEvent _enableAttackColliderEvent = new AnimationEvent();

    [SerializeField]
    protected EdgeCollider2D _attackCollider;
    [SerializeField]
    protected int _frameRateInAttackAnimationForEnableAttackCollider;
    [SerializeField]
    protected AnimationClip _attackAnimation;

    public override EnemyAttackState AttackState { get; protected set; } = new EnemyAttackMeleeState();
    public override EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyMeleeDetectTargetState();

    private new void Awake() {
        base.Awake();
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