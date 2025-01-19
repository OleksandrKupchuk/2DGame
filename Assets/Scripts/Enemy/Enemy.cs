using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : Character {
    protected float _xScale;

    public float CurrentHealth { get; protected set; }
    public bool IsDead { get => CurrentHealth <= 0; }
    public bool IsLookRight { get => transform.localScale.x > 0; }
    public bool IsLookLeft { get => transform.localScale.x < 0; }
    public bool IsTargetBehindYouWhenLookRight { get => (transform.position.x - FieldOfView.Target.transform.position.x) >= 0 && IsLookRight; }
    public bool IsTargetBehindYouWhenLookLeft { get => (transform.position.x - FieldOfView.Target.transform.position.x) < 0 && IsLookLeft; }
    public float GetDirectionX { get => transform.localScale.x; }
    public FieldOfView FieldOfView { get; protected set; }
    public bool HasTarget { get => FieldOfView.Target != null; }
    public EnemyConfig Config { get => (EnemyConfig)_config; }
    public StateMachine<Enemy> StateMachine { get; protected set; }
    public virtual EnemyIdleState IdleState { get; protected set; } = new EnemyIdleState();
    public virtual EnemyRunState RunState { get; protected set; } = new EnemyRunState();
    public virtual EnemyDetectTargetState DetectTarget { get; protected set; } = new EnemyDetectTargetState();
    public virtual EnemyAttackState AttackState { get; protected set; } = new EnemyAttackState();
    public virtual EnemyHitState HiState { get; protected set; } = new EnemyHitState();
    public virtual EnemyDeadState DeadState { get; protected set; } = new EnemyDeadState();

    [HideInInspector]
    public float delayAttack;
    [HideInInspector]
    public float delayStrikeAttack;
    [HideInInspector]
    public float distanceToTarget;

    protected new void Awake() {
        base.Awake();
        CurrentHealth = _config.health;
        _xScale = transform.localScale.x;
        GameObject _fieldOfViewPrefab = Resources.Load(ResourcesPath.FieldOfViewPrefab) as GameObject;
        FieldOfView = Instantiate(_fieldOfViewPrefab.GetComponent<FieldOfView>());
        StateMachine = new StateMachine<Enemy>(this);
    }

    public void Flip() {
        _xScale *= -1;
        gameObject.transform.localScale = new Vector3(_xScale, transform.localScale.y, transform.localScale.z);
    }

    public bool IsNeedLookOnPlayer() {
        if(FieldOfView.Target == null) {
            //print("Target is NULL");
            return false;
        }
        if (IsTargetBehindYouWhenLookRight) {
            return true;
        }
        else if (IsTargetBehindYouWhenLookLeft) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool IsThereTargetInRangeOfDistance(float distance) {
        if(distanceToTarget <= distance) {
            return true;
        }

        return false;
    }

    public void EnableCollider(Collider2D collider2D) {
        collider2D.enabled = true;
    }

    public void DisableCollider(Collider2D collider2D) {
        collider2D.enabled = false;
    }

    public void TakeDamage(float damage) {
        //print("player damage = " + damage);
        float _clearDamage = damage - GetBlockedDamage(_config.armor);
        if (_clearDamage <= 0) {
            return;
        }
        //print("clear take enemy damage = " + _clearDamage);
        CurrentHealth -= _clearDamage;

        if (IsDead) {
            StateMachine.ChangeState(DeadState);
        }
        else {
            StateMachine.ChangeState(HiState);
        }
    }
}
