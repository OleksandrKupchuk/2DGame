using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    private IInteracvite _interactive;
    private List<Damage> _objectsAttack = new List<Damage>();
    private RaycastHit2D _raycastHit;
    private float _deafaultGravityScale;
    private float _timeRegenerationHealth;
    private float _timerHealthRegeneration;
    private AnimationEvent _attackEvent = new AnimationEvent();
    private PlayerHealthView _playerHealthView;
    private float _currentHealth;

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

    public float CurrentHealth { get => _currentHealth = _currentHealth > PlayerAttributes.Health ? PlayerAttributes.Health : _currentHealth; }
    public bool IsDead { get => CurrentHealth <= 0; }
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
    [field: SerializeField]
    public InvulnerabilityStatus InvulnerableStatus { get; private set; }
    public Inventory Inventory { get; private set; }
    public PlayerAttributes PlayerAttributes { get; private set; }
    public IInteracvite Interactive { get => _interactive; }
    public PlayerQuestSystem QuestSystem { get; private set; } = new PlayerQuestSystem();
    [field: SerializeField]
    public PlayerMovement PlayerMovement { get; private set; }

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
        PlayerMovement.Init(Config);
        _deafaultGravityScale = Rigidbody.gravityScale;
        Inventory = FindObjectOfType<Inventory>();
        PlayerAttributes = FindObjectOfType<PlayerAttributes>();
        CheckComponentOnNull();
        DisableSwordCollider();
        _currentHealth = _config.health;
    }

    private void CheckComponentOnNull() {
        if (InvulnerableStatus == null) {
            Debug.LogError($"Component {nameof(InvulnerabilityStatus)} is null");
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
        _playerHealthView = FindAnyObjectByType<PlayerHealthView>();
        _playerHealthView.UpdateHealthBar(null);
    }

    private void Update() {
        StateMachine.Update();
        RegenerationHealth();
        ToggleInventory();
    }

    private void FixedUpdate() {
        StateMachine.FixedUpdate();
    }

    public void CheckTakeDamage(float damage, Damage damageObject) {
        if (IsThisAlreadyAttacked(damageObject)) {
            StartCoroutine(ResetCurrentDamage(damageObject));
        }
        else if (InvulnerableStatus.IsInvulnerability) {
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
        float _cleanDamage = damage - GetBlockedDamage(PlayerAttributes.Armor);
        //print("clear damage = " + _clearDamage);
        if (_cleanDamage <= 0) {
            return;
        }

        _currentHealth -= _cleanDamage;
        _playerHealthView.UpdateHealthBar(null);

        //print("health = " + _health);
        if (IsDead) {
            StateMachine.ChangeState(DeadState);
        }
        else {
            StateMachine.ChangeState(HitState);
        }
    }

    public void SetGravityScale(float value) {
        Rigidbody.gravityScale = value;
    }

    public void ResetGravityScaleToDefault() {
        Rigidbody.gravityScale = _deafaultGravityScale;
    }

    private void RegenerationHealth() {
        if (Animator.GetCurrentAnimatorStateInfo(AnimatorLayers.BaseLayer).IsName(PlayerAnimationName.Hit)) {
            _timerHealthRegeneration = 0;
        }

        _timerHealthRegeneration += Time.deltaTime;

        if (_timerHealthRegeneration >= Config.delayHealthRegeneration) {
            if (CurrentHealth >= PlayerAttributes.Health) {
                return;
            }

            _timeRegenerationHealth += Time.deltaTime;

            if (_timeRegenerationHealth >= 1) {
                _timeRegenerationHealth = 0;
                AddHealth(PlayerAttributes.HealthRegeneration);
                //_currentHealth += PlayerAttributes.HealthRegeneration;
                Debug.Log($"regenration health + <color=green>{PlayerAttributes.HealthRegeneration}</color>");
                Debug.Log($"health after healing + <color=blue>{PlayerAttributes.Health}</color>");
            }
        }
    }

    public void AddHealth(float health) {
        _currentHealth += health;
        _playerHealthView.UpdateHealthBar(null);
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

    private void ToggleInventory() {
        if (PlayerMovement.IsOpenInventory) {
            Inventory.ActiveToggle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out IInteracvite interacvite)) {
            print("interactive set" + interacvite.GetType().Name);
            _interactive = interacvite;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.TryGetComponent(out IInteracvite interacvite)) {
            print("interactive null");
            _interactive = null;
        }
    }
}
