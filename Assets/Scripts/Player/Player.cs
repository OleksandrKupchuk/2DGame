using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character {
    private List<Damage> _objectsAttack = new List<Damage>();
    private RaycastHit2D _raycastHit;
    private float _deafaultGravityScale;
    private float _timeRegenerationHealth;
    private float _delayHealthRegeneration;
    private AnimationEvent _attackEvent = new AnimationEvent();

    [SerializeField]
    private InputActionReference _movementInputAction;
    [SerializeField]
    private InputActionReference _shotInputAction;
    [SerializeField]
    private InputActionReference _jumpInputAction;
    [SerializeField]
    private InvulnerabilityAnimation _invulnerableStatus;
    [SerializeField]
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private float _distanceRaycastHit;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private List<Collider2D> _collidersForIgnored = new List<Collider2D>();
    [SerializeField]
    private PlayerSword _playerSword;
    [SerializeField]
    protected int _frameRateInAttackAnimationForEnableCollider;
    [SerializeField]
    protected int _frameRateInAttackAnimationForDisableCollider;
    [SerializeField]
    protected AnimationClip _attackAnimation;

    public float CurrentHealth { get; protected set; }
    public bool IsDead { get => CurrentHealth <= 0; }
    public bool IsLookingLeft { get => transform.localScale.x > 0; }
    public bool IsAttack {
        get {
            if (ShotInputAction.action.IsPressed()) {
                return true;
            }
            return false;
        }
    }
    public bool IsFalling { get => Rigidbody.velocity.y < 0; }
    public bool CanJump {
        get {
            if (JumpInputAction.action.triggered && IsGround()) {
                //print("can jump");
                return true;
            }

            return false;
        }
    }
    public Vector2 MovementInput { get; private set; }
    public List<Collider2D> CollidersForIgnored { get => _collidersForIgnored; }
    public PlayerConfig Config { get => (PlayerConfig)_config; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerJumpUpState JumpUpState { get; private set; }
    public PlayerJumpDownState JumpDownState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }
    public InputActionReference ShotInputAction { get => _shotInputAction; }
    public InputActionReference JumpInputAction { get => _jumpInputAction; }
    public InvulnerabilityAnimation InvulnerableStatus { get => _invulnerableStatus; }
    public Inventory Inventory { get; private set; }
    public PlayerAttributes PlayerAttributes { get; private set; }

    private new void Awake() {
        base.Awake();
        IdleState = new PlayerIdleState();
        RunState = new PlayerRunState();
        AttackState = new PlayerAttackState();
        JumpUpState = new PlayerJumpUpState();
        JumpDownState = new PlayerJumpDownState();
        HitState = new PlayerHitState();
        DeadState = new PlayerDeadState();
        StateMachine = new StateMachine<Player>(this);
        _deafaultGravityScale = Rigidbody.gravityScale;
        Inventory = FindObjectOfType<Inventory>();
        PlayerAttributes = FindObjectOfType<PlayerAttributes>();
        CheckComponentOnNull();
        DisableSwordCollider();
        EventManager.UpdateAttributes += CalculationCurrentHealth;
    }

    private void OnDestroy() {
        EventManager.UpdateAttributes -= CalculationCurrentHealth;
    }

    private void CalculationCurrentHealth() {
        CurrentHealth = CurrentHealth > PlayerAttributes.Health ? PlayerAttributes.Health : CurrentHealth;
    }

    private void CheckComponentOnNull() {
        if (_movementInputAction == null) {
            Debug.LogError($"Component {nameof(InputActionReference)} is null");
        }
        if (_shotInputAction == null) {
            Debug.LogError($"Component {nameof(InputActionReference)} is null");
        }
        if (_jumpInputAction == null) {
            Debug.LogError($"Component {nameof(InputActionReference)} is null");
        }
        if (_invulnerableStatus == null) {
            Debug.LogError($"Component {nameof(InvulnerabilityAnimation)} is null");
        }
        if (_boxCollider == null) {
            Debug.LogError($"Component {nameof(BoxCollider2D)} is null");
        }
        if (Inventory == null) {
            Debug.LogError($"Component {nameof(Inventory)} is null");
        }
        if (PlayerAttributes == null) {
            Debug.LogError($"object {nameof(PlayerAttributes)} is null");
            return;
        }
    }

    private void Start() {
        StateMachine.ChangeState(IdleState);
    }

    private void Update() {
        IsGround();
        StateMachine.Update();
        RegenerationHealth();
    }

    private void FixedUpdate() {
        StateMachine.FixedUpdate();
    }

    public Vector2 GetMovementInput() {
        return MovementInput = _movementInputAction.action.ReadValue<Vector2>();
    }

    public void Flip() {
        if (MovementInput.x > 0) {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (MovementInput.x < 0) {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Jump() {
        //print("Add Force for Jump");
        Rigidbody.velocity = Vector2.up * Config.jumpVelocity;
    }

    public void CheckTakeDamage(float damage, Damage damageObject) {
        if (IsThisAlreadyAttacked(damageObject)) {
            StartCoroutine(ResetCurrentDamage(damageObject));
        }
        else if (_invulnerableStatus.IsInvulnerability) {
            //print("player is invulnerable");
        }
        else {
            RegisterDamageObject(damageObject);
            TakeDamage(damage);
        }
    }

    private bool IsThisAlreadyAttacked(Damage damageObject) {
        if (_objectsAttack.Count == 0) {
            return false;
        }

        foreach (var attack in _objectsAttack) {
            if (attack == damageObject) {
                //print("this object already attacked");
                return true;
            }
        }

        return false;
    }

    private IEnumerator ResetCurrentDamage(Damage damageObject) {
        yield return new WaitForSeconds(2);
        _objectsAttack.Remove(damageObject);
    }

    private void RegisterDamageObject(Damage damageObject) {
        _objectsAttack.Add(damageObject);
    }

    public void TakeDamage(float damage) {
        //print("damage = " + damage);
        float _clearDamage = damage - GetBlockedDamage(PlayerAttributes.Armor);
        //print("clear damage = " + _clearDamage);
        if (_clearDamage <= 0) {
            return;
        }

        CurrentHealth -= _clearDamage;
        EventManager.UpdateAttributesEventHandler();
        //print("health = " + _health);
        if (IsDead) {
            StateMachine.ChangeState(DeadState);
        }
        else {
            StateMachine.ChangeState(HitState);
        }
    }

    public bool IsGround() {
        Color _color;
        _raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, _distanceRaycastHit, _groundLayer);
        if (_raycastHit.transform != null) {
            _color = Color.green;
        }
        else {
            _color = Color.red;
        }
        Debug.DrawRay(_boxCollider.bounds.center + new Vector3(_boxCollider.bounds.extents.x, 0), Vector3.down * (_boxCollider.bounds.extents.y + _distanceRaycastHit), _color);
        Debug.DrawRay(_boxCollider.bounds.center - new Vector3(_boxCollider.bounds.extents.x, 0), Vector3.down * (_boxCollider.bounds.extents.y + _distanceRaycastHit), _color);
        Debug.DrawRay(_boxCollider.bounds.center - new Vector3(_boxCollider.bounds.extents.x, _boxCollider.bounds.extents.y + _distanceRaycastHit), Vector3.right * _boxCollider.bounds.size.x, _color);
        return _raycastHit.transform != null;
    }

    public void SetGravityScale(float value) {
        Rigidbody.gravityScale = value;
    }

    public void ResetGravityScaleToDefault() {
        Rigidbody.gravityScale = _deafaultGravityScale;
    }

    private void RegenerationHealth() {
        if (Animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).IsName(PlayerAnimationName.Hit)) {
            _delayHealthRegeneration = 0;
        }

        _delayHealthRegeneration += Time.deltaTime;

        if (_delayHealthRegeneration >= Config.delayHealthRegeneration) {
            if (CurrentHealth >= PlayerAttributes.Health) {
                return;
            }

            _timeRegenerationHealth += Time.deltaTime;

            if (_timeRegenerationHealth >= 1) {
                _timeRegenerationHealth = 0;
                AddHealth(PlayerAttributes.HealthRegeneration);
                Debug.Log($"regenration health + <color=green>{PlayerAttributes.HealthRegeneration}</color>");
                Debug.Log($"health after healing + <color=blue>{PlayerAttributes.Health}</color>");
            }
        }
    }

    public void AddHealth(float health) {
        CurrentHealth += health;
        CurrentHealth = CurrentHealth > PlayerAttributes.Health ? PlayerAttributes.Health : CurrentHealth;
        EventManager.UpdateAttributesEventHandler();
    }

    public void AddEnableSwordColliderEventForAttackAnimation() {
        AttachingEventToAnimation.AddEventForFrameOfAnimation(_attackAnimation, _attackEvent, _frameRateInAttackAnimationForEnableCollider, nameof(EnableSwordCollider));
    }

    public void AddDisableSwordColliderEventForAttackAnimation() {
        AttachingEventToAnimation.AddEventForFrameOfAnimation(_attackAnimation, _attackEvent, _frameRateInAttackAnimationForDisableCollider, nameof(DisableSwordCollider));
    }

    private void EnableSwordCollider() {
        _playerSword.BoxCollider2D.enabled = true;
    }

    private void DisableSwordCollider() {
        _playerSword.BoxCollider2D.enabled = false;
    }
}
