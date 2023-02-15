using UnityEngine;

public class EnemyOfMelee : BasicOfLogicEnemy {

    public StateMachine<EnemyOfMelee> StateMachine { get; protected set; }
    public virtual EnemyIdleState IdleState { get; protected set; } = new EnemyIdleState();
    public virtual EnemyRunState RunState { get; protected set; } = new EnemyRunState();
    public virtual EnemyMeleeAttackState AttckState { get; protected set; } = new EnemyMeleeAttackState();
    public virtual EnemyHitState HiState { get; protected set; } = new EnemyHitState();
    public virtual EnemyDeadState DeadState { get; protected set; } = new EnemyDeadState();

    protected new void Awake() {
        GameObject _fieldOfViewPrefab = Resources.Load(ResourcesPath.FieldOfViewPrefab) as GameObject;
        FieldOfView = Instantiate(_fieldOfViewPrefab.GetComponent<FieldOfView>());
        base.Awake();
        StateMachine = new StateMachine<EnemyOfMelee>(this);
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
}
