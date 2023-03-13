using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LogicEnemyOfRange))]
[RequireComponent(typeof(IgnoreCollision))]
public class EnemyDragorWarrior : BasicEnemy {

    protected AnimationEvent _shotFireBallEvent = new AnimationEvent();
    protected AnimationEvent _enableStrikeColliderEvent = new AnimationEvent();
    protected LogicEnemyOfRange _logicEnemyOfRange;
    private IgnoreCollision _ignoreCollision;

    [SerializeField]
    protected FireBall _fireBallPrefab;
    [SerializeField]
    protected Transform _parentFireBalls;
    [SerializeField]
    protected Transform _shotPoint;
    [SerializeField]
    protected int _frameRateInAttackAnimationForEnableFireBall;
    [SerializeField]
    protected AnimationClip _attackAnimation;
    [SerializeField]
    protected int _frameRateInStrikeAnimationForEnableCollider;
    [SerializeField]
    protected AnimationClip _strikeAnimation;
    [SerializeField]
    private Collider2D _strikeCollider;
    [SerializeField]
    private Collider2D _bodyCollider;

    public float AttackRangeDistance { get; protected set; }
    public float StrikeAttackDistance { get; private set; }
    public virtual bool IsCloseTargetToStrikeAttack { get => distanceToTarget <= StrikeAttackDistance; }
    public List<FireBall> FireBalls { get; protected set; } = new List<FireBall>();
    public Transform ShotPoint { get => _shotPoint; }
    public override EnemyAttackState AttackState { get; protected set; } = new EnemyAttackRangeState();
    public override EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyDragonWarriorDetectTargetState();
    public EnemyDragonWarriorStrikeState StrikeState { get; protected set; } = new EnemyDragonWarriorStrikeState();

    protected new void Awake() {
        base.Awake();
        _logicEnemyOfRange = GetComponent<LogicEnemyOfRange>();
        _ignoreCollision = GetComponent<IgnoreCollision>();
        AttackRangeDistance = 12f;
        StrikeAttackDistance = 9f;
        DisableStrikeCollider();
        FireBalls = _logicEnemyOfRange.CreateAndGetListPrefabs(_fireBallPrefab, _parentFireBalls);
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

    public void AddEnableFireBallEventForAttackAnimation() {
        LogicEnemy.AddEventForFrameOfAnimation(_attackAnimation, _shotFireBallEvent, _frameRateInAttackAnimationForEnableFireBall, nameof(EnbaleFireBallForEvent));
    }

    private void EnbaleFireBallForEvent() {
        _logicEnemyOfRange.SetPrefabDirectionShotPointAndEnable(FireBalls, ShotPoint, GetDirectionX);
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
