using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LogicEnemyOfRange))]
public class EnemyOfRange : Enemy {

    protected AnimationEvent _enableAttackColliderEvent = new AnimationEvent();
    protected LogicEnemyOfRange _logicEnemyOfRange;

    [SerializeField]
    protected GameObject _fireBallPrefab;
    [SerializeField]
    protected Transform _parentFireBalls;
    [SerializeField]
    protected Transform _shotPoint;
    [SerializeField]
    protected int _frameRateInAttackAnimationForEnableFireBall;
    [SerializeField]
    protected AnimationClip _attackAnimation;

    public List<GameObject> FireBallsPrefabs { get; protected set; } = new List<GameObject>();
    public List<FireBall> FireBalls { get; protected set; } = new List<FireBall>();
    public Transform ShotPoint { get => _shotPoint; }
    public override EnemyAttackState AttackState { get; protected set; } = new EnemyAttackRangeState();
    public override EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyRangeDetectTargetState();

    protected new void Awake() {
        base.Awake();
        _logicEnemyOfRange = GetComponent<LogicEnemyOfRange>();
        FireBallsPrefabs = _logicEnemyOfRange.CreateAndGetListPrefabs(_fireBallPrefab, _parentFireBalls);
    }

    private void Start() {
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
        LogicEnemy.AddEventToEndOfAnimation(_attackAnimation, _enableAttackColliderEvent, nameof(EnbaleFireBallForEvent));
    }

    private void EnbaleFireBallForEvent() {
        _logicEnemyOfRange.SetPrefabDirectionShotPointAndEnable(FireBallsPrefabs, ShotPoint, GetDirectionX);
    }
}
