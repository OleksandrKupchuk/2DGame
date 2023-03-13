using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LogicEnemyOfRange))]
[RequireComponent(typeof(IgnoreCollision))]
public class EnemyKnight : BasicEnemy {

    protected AnimationEvent _enableAttackColliderEvent = new AnimationEvent();
    protected AnimationEvent _enableStrikeColliderEvent = new AnimationEvent();
    protected AnimationEvent _shotFireBallEvent = new AnimationEvent();
    protected LogicEnemyOfRange _logicEnemyOfRange;
    private IgnoreCollision _ignoreCollision;

    [SerializeField]
    protected EdgeCollider2D _attackCollider;
    [SerializeField]
    protected int _frameRateInAttackAnimationForEnableAttackCollider;
    [SerializeField]
    protected AnimationClip _attackAnimation;
    [SerializeField]
    protected FireBall _fireBallPrefab;
    [SerializeField]
    protected Transform _parentFireBalls;
    [SerializeField]
    protected Transform _castPoint;
    [SerializeField]
    protected int _frameRateInCastAnimationForEnableFireBall;
    [SerializeField]
    protected AnimationClip _castAnimation;
    [SerializeField]
    protected int _frameRateInStrikeAnimationForEnableCollider;
    [SerializeField]
    protected AnimationClip _strikeAnimation;
    [SerializeField]
    private Collider2D _strikeCollider;
    [SerializeField]
    private Collider2D _bodyCollider;

    public bool StrikeCollider { get => _strikeCollider.enabled; }
    public float AttackMeleeDistance { get; protected set; }
    public virtual bool IsLeftHalfOfHealth { get => _health <= _maxHealth / 2; }
    public List<FireBall> FireBalls { get; protected set; } = new List<FireBall>();
    public Transform CastPoint { get => _castPoint; }
    public override EnemyAttackState AttackState { get; protected set; } = new EnemyAttackMeleeState();
    public override EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyKnightDetectTargetState();
    public EnemyKnightStrikeState StrikeState { get; protected set; } = new EnemyKnightStrikeState();

    [HideInInspector]
    public float delayStrikeAttack;

    protected new void Awake() {
        base.Awake();
        _health = 4f;
        _logicEnemyOfRange = GetComponent<LogicEnemyOfRange>();
        _ignoreCollision = GetComponent<IgnoreCollision>();
        AttackMeleeDistance = 3f;
        DisableStrikeCollider();
        FireBalls = _logicEnemyOfRange.CreateAndGetListPrefabs(_fireBallPrefab, _parentFireBalls);
        DisableAttackCollider();
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

    public void AddEnableAttackColliderForAttackAnimation() {
        LogicEnemy.AddEventForFrameOfAnimation(_attackAnimation, _enableAttackColliderEvent, _frameRateInAttackAnimationForEnableAttackCollider, nameof(EnableAttackCollider));
    }

    private void EnableAttackCollider() {
        LogicEnemy.EnableCollider(_attackCollider);
    }

    public void AddDisableAttackCoolliderEventForAttackAnimation() {
        LogicEnemy.AddEventToEndOfAnimation(_attackAnimation, _enableAttackColliderEvent, nameof(DisableAttackCollider));
    }

    public void DisableAttackCollider() {
        LogicEnemy.DisableCollider(_attackCollider);
    }

    public void AddEnableFireBallEventForCastAnimation() {
        LogicEnemy.AddEventForFrameOfAnimation(_castAnimation, _shotFireBallEvent, _frameRateInCastAnimationForEnableFireBall, nameof(EnbaleFireBallForEvent));
    }

    private void EnbaleFireBallForEvent() {
        _logicEnemyOfRange.SetPrefabDirectionShotPointAndEnable(FireBalls, CastPoint, GetDirectionX);
    }

    public void AddEnableStrikeColliderForStrikeAnimation() {
        LogicEnemy.AddEventForFrameOfAnimation(_strikeAnimation, _enableStrikeColliderEvent, _frameRateInStrikeAnimationForEnableCollider, nameof(EnableStrikeCollider));
    }

    private void EnableStrikeCollider() {
        LogicEnemy.EnableCollider(_strikeCollider);
    }

    public void DisableStrikeCollider() {
        LogicEnemy.DisableCollider(_strikeCollider);
    }
}
