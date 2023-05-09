using UnityEngine;

[RequireComponent(typeof(LogicEnemyOfRange))]
[RequireComponent(typeof(IgnoreCollision))]
public class EnemyRogue : Enemy {
    protected AnimationEvent _enableAttackLeftHandUpColliderEvent = new AnimationEvent();
    protected AnimationEvent _enableAttackRightUpColliderEvent = new AnimationEvent();
    protected LogicEnemyOfRange _logicEnemyOfRange;
    private IgnoreCollision _ignoreCollision;

    [SerializeField]
    protected Collider2D _colliderRightKnife;
    [SerializeField]
    protected Collider2D _colliderLeftKnife;
    [SerializeField]
    protected int _frameRateInAttackLeftHandUpAnimationForEnableAttackCollider;
    [SerializeField]
    protected int _frameRateInAttackRightHandUpAnimationForEnableAttackCollider;
    [SerializeField]
    protected int _frameRateInAttackJumpUpAnimationForEnableAttackCollider;
    [SerializeField]
    protected AnimationClip _attackLeftHandUpAnimation;
    [SerializeField]
    protected AnimationClip _attackRightHandUpAnimation;
    [SerializeField]
    protected AnimationClip _attackJumpUpAnimation;
    [SerializeField]
    private Collider2D _bodyCollider;

    public override EnemyAttackState AttackState { get; protected set; } = new EnemyRogueAttacLeftHandUpkState();
    public EnemyAttackState AttackRightHandUp { get; protected set; } = new EnemyRougeAttackRightHandUpState();
    public EnemyAttackState AttackJumpUp { get; protected set; } = new EnemyRougeAttackJumpUpState();
    public override EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyRogueDetectTargetState();

    protected new void Awake() {
        base.Awake();
        _currentHealth = 4f;
        _logicEnemyOfRange = GetComponent<LogicEnemyOfRange>();
        _ignoreCollision = GetComponent<IgnoreCollision>();
        DisableColliderRightKnife();
        DisableColliderLeftKnife();
    }

    private void Start() {
        _ignoreCollision.IgnorePlayerColliders(_bodyCollider);
        StateMachine.ChangeState(IdleState);
    }

    private void Update() {
        StateMachine.Update();
        FieldOfView.SetStartPoint(transform.position);
    }

    private void FixedUpdate() {
        StateMachine.FixedUpdate();
    }

    public void AddEnableAttackLeftHandUpColliderForAttackLeftHandUpAnimation() {
        LogicEnemy.AddEventForFrameOfAnimation(_attackLeftHandUpAnimation, _enableAttackLeftHandUpColliderEvent, _frameRateInAttackLeftHandUpAnimationForEnableAttackCollider, nameof(EnableColliderLeftKnife));
    }

    public void AddEnableAttackRightHandUpColliderForAttackLeftHandUpAnimation() {
        LogicEnemy.AddEventForFrameOfAnimation(_attackRightHandUpAnimation, _enableAttackRightUpColliderEvent, _frameRateInAttackRightHandUpAnimationForEnableAttackCollider, nameof(EnableColliderRightKnife));
    }

    private void EnableColliderRightKnife() {
        LogicEnemy.EnableCollider(_colliderRightKnife);
    }

    private void EnableColliderLeftKnife() {
        LogicEnemy.EnableCollider(_colliderLeftKnife);
    }

    public void DisableColliderRightKnife() {
        LogicEnemy.DisableCollider(_colliderRightKnife);
    }

    public void DisableColliderLeftKnife() {
        LogicEnemy.DisableCollider(_colliderLeftKnife);
    }
}