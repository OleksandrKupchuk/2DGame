using UnityEngine;

public class EnemyOfMelee : BasicOfLogicEnemy {

    protected AnimationEvent _enableAttackColliderEvent = new AnimationEvent();

    [SerializeField]
    protected EdgeCollider2D _attackCollider;
    [SerializeField]
    protected int _frameRateInAttackAnimationForEnableAttackCollider;
    [SerializeField]
    protected AnimationClip _attackAnimation;

    public float DistanceToMeleeAttack { get; private set; }
    public StateMachine<EnemyOfMelee> StateMachine { get; protected set; }
    public virtual EnemyIdleState IdleState { get; protected set; } = new EnemyIdleState();
    public virtual EnemyRunState RunState { get; protected set; } = new EnemyRunState();
    public virtual EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyDetectTargetState();
    public virtual EnemyAttackMeleeState AttackMeleeState { get; protected set; } = new EnemyAttackMeleeState();
    public virtual EnemyHitState HiState { get; protected set; } = new EnemyHitState();
    public virtual EnemyDeadState DeadState { get; protected set; } = new EnemyDeadState();

    protected new void Awake() {
        DistanceToMeleeAttack = 3.2f;
        GameObject _fieldOfViewPrefab = Resources.Load(ResourcesPath.FieldOfViewPrefab) as GameObject;
        FieldOfView = Instantiate(_fieldOfViewPrefab.GetComponent<FieldOfView>());
        base.Awake();
        StateMachine = new StateMachine<EnemyOfMelee>(this);
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
        float _playingAnimationTime = _frameRateInAttackAnimationForEnableAttackCollider / _attackAnimation.frameRate;
        _enableAttackColliderEvent.time = _playingAnimationTime;
        _enableAttackColliderEvent.functionName = nameof(EnableAttackCollider);

        _attackAnimation.AddEvent(_enableAttackColliderEvent);
    }

    public void AddDisableAttackCoolliderEventForAttackAnimation() {
        float _playingAnimationTime = _attackAnimation.length;
        _enableAttackColliderEvent.time = _playingAnimationTime;
        _enableAttackColliderEvent.functionName = nameof(DisableAttackCollider);

        _attackAnimation.AddEvent(_enableAttackColliderEvent);
    }

    protected void EnableAttackCollider() {
        _attackCollider.enabled = true;
    }

    protected void DisableAttackCollider() {
        _attackCollider.enabled = false;
    }
}
