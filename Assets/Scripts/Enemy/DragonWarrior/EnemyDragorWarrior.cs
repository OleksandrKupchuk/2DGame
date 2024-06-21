using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Projectile))]
[RequireComponent(typeof(IgnoreCollision))]
public class EnemyDragorWarrior : Enemy {

    protected AnimationEvent _shotFireBallEvent = new AnimationEvent();
    protected AnimationEvent _enableStrikeColliderEvent = new AnimationEvent();
    protected AnimationEvent _moveStrikeEvent = new AnimationEvent();
    protected Projectile _projectile;
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

    [HideInInspector]
    public bool canMoveStrike;

    public List<FireBall> FireBalls { get; protected set; } = new List<FireBall>();
    public Transform ShotPoint { get => _shotPoint; }
    public override EnemyAttackState AttackState { get; protected set; } = new EnemyAttackRangeState();
    public override EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyDragonWarriorDetectTargetState();
    public EnemyDragonWarriorStrikeState StrikeState { get; protected set; } = new EnemyDragonWarriorStrikeState();

    protected new void Awake() {
        base.Awake();
        _projectile = GetComponent<Projectile>();
        _ignoreCollision = GetComponent<IgnoreCollision>();
        DisableStrikeCollider();
        FireBalls = _projectile.CreateAndGetListPrefabs(_fireBallPrefab, _parentFireBalls, 5);
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
        AttachingEventToAnimation.AddEventForFrameOfAnimation(_attackAnimation, _shotFireBallEvent, _frameRateInAttackAnimationForEnableFireBall, nameof(EnbaleFireBallForEvent));
    }

    private void EnbaleFireBallForEvent() {
        _projectile.SetDirectionShotPointAndEnable(FireBalls, ShotPoint, GetDirectionX);
    }

    public void AddEnableStrikeColliderForStrikeAnimation() {
        AttachingEventToAnimation.AddEventForFrameOfAnimation(_strikeAnimation, _enableStrikeColliderEvent, _frameRateInStrikeAnimationForEnableCollider, nameof(EnableStrikeCollider));
    }

    private void EnableStrikeCollider() {
        EnableCollider(_strikeCollider);
    }

    public void DisableStrikeCollider() {
        DisableCollider(_strikeCollider);
    }

    public void AddCanMoveStrikeTrue() {
        AttachingEventToAnimation.AddEventForFrameOfAnimation(_strikeAnimation, _moveStrikeEvent, _frameRateInStrikeAnimationForEnableCollider, nameof(CanMoveStrikeTrue));
    }

    private void CanMoveStrikeTrue() {
        canMoveStrike = true;
    }
}
