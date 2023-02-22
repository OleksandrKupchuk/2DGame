using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class BasicEnemy : BaseCharacteristics {

    protected float _xScale;

    public float GetLocalScaleX { get => transform.localScale.x; }
    public FieldOfView FieldOfView { get; protected set; }
    public float AttackDistance { get; protected set; }
    public LogicEnemy LogicEnemy { get; private set; } = new LogicEnemy();

    public StateMachine<BasicEnemy> StateMachine { get; protected set; }
    public virtual EnemyIdleState IdleState { get; protected set; } = new EnemyIdleState();
    public virtual EnemyRunState RunState { get; protected set; } = new EnemyRunState();
    public virtual EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyDetectTargetState();
    public virtual EnemyAttackMeleeState AttackState { get; protected set; } = new EnemyAttackMeleeState();
    public virtual EnemyHitState HiState { get; protected set; } = new EnemyHitState();
    public virtual EnemyDeadState DeadState { get; protected set; } = new EnemyDeadState();

    public virtual bool IsThereTargetInRangeOfAttack { get => distanceToTarget <= AttackDistance; }

    [HideInInspector]
    public float distanceToTarget;
    [HideInInspector]
    public float delayAttack;

    protected new void Awake() {
        base.Awake();
        _xScale = transform.localScale.x;
        GameObject _fieldOfViewPrefab = Resources.Load(ResourcesPath.FieldOfViewPrefab) as GameObject;
        FieldOfView = Instantiate(_fieldOfViewPrefab.GetComponent<FieldOfView>());
        StateMachine = new StateMachine<BasicEnemy>(this);
    }

    public void Flip() {
        _xScale *= -1;
        gameObject.transform.localScale = new Vector3(_xScale, transform.localScale.y, transform.localScale.z);
    }

    public bool IsNeedLookOnPlayer() {
        if ((transform.position.x - FieldOfView.Target.transform.position.x) < 0 && transform.localScale.x == -1) {
            return true;
        }
        else if ((transform.position.x - FieldOfView.Target.transform.position.x) > 0 && transform.localScale.x == 1) {
            return true;
        }
        else {
            return false;
        }
    }
}
