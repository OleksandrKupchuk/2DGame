using System.Collections.Generic;
using UnityEngine;

public class EnemyOfRange : BasicEnemy {

    protected AnimationEvent _enableAttackColliderEvent = new AnimationEvent();

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
    public override EnemyAttackMeleeState AttackState { get; protected set; } = new EnemyAttackRangeState();

    protected new void Awake() {
        base.Awake();
        AttackDistance = 12f;
        FireBallsPrefabs = CreateAndGetListFireBalls(_fireBallPrefab, _parentFireBalls);
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
        EnableFireBall(FireBallsPrefabs, ShotPoint, GetLocalScaleX);
    }

    public List<GameObject> CreateAndGetListFireBalls(GameObject prefab, Transform parentTransform) {
        List<GameObject> _fireBalls = new List<GameObject>();

        for (int i = 0; i < 5; i++) {
            GameObject _fireBall = Instantiate(prefab);
            _fireBall.transform.SetParent(parentTransform);
            _fireBall.SetActive(false);
            _fireBalls.Add(_fireBall);
        }

        return _fireBalls;
    }

    public void EnableFireBall(List<GameObject> fireBalls, Transform shotPoint, float direction) {
        foreach (var fireBall in fireBalls) {
            if (!fireBall.activeSelf) {
                fireBall.transform.position = shotPoint.transform.position;
                fireBall.transform.SetParent(null);
                fireBall.transform.localScale = new Vector3(direction, fireBall.transform.localScale.y, fireBall.transform.localScale.z);
                fireBall.SetActive(true);
                return;
            }
        }
    }
}
