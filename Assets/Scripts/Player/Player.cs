using System.Collections.Generic;
using UnityEngine;

public class Player : Character {
    private IInteracvite _interactive;
    private float _deafaultGravityScale;
    private PlayerHealthView _playerHealthView;

    [SerializeField]
    private List<Collider2D> _collidersForIgnored = new List<Collider2D>();

    public List<Collider2D> CollidesForIgnored { get => _collidersForIgnored; }
    public PlayerConfig Config { get => (PlayerConfig)_config; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerJumpUpState JumpUpState { get; private set; }
    public PlayerJumpDownState JumpDownState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }
    public Inventory Inventory { get; private set; }
    public PlayerAttributes PlayerAttributes { get; private set; }
    public QuestSystem QuestSystem { get; private set; }

    public IInteracvite Interactive { get => _interactive; }
    [field: SerializeField]
    public InvulnerabilityStatus InvulnerableStatus { get; private set; }
    [field: SerializeField]
    public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField]
    public HealthController HealthController { get; private set; }
    [field: SerializeField]
    public PlayerWeaponController PlayerWeaponController { get; private set; }
    [field: SerializeField]
    public PickUpController PickUpController { get; private set; }

    private new void Awake() {
        base.Awake();
        PlayerAttributes = FindObjectOfType<PlayerAttributes>();
        PlayerMovement.Init(Config);
        HealthController.Init(Config, PlayerAttributes);
        PlayerWeaponController.Init();
        _deafaultGravityScale = Rigidbody.gravityScale;
        Inventory = FindObjectOfType<Inventory>();
        QuestSystem = new QuestSystem();
        PickUpController.Init(Inventory, QuestSystem);
        EventManager.OnHit += () => StateMachine.ChangeState(HitState);
        EventManager.OnDead += () => StateMachine.ChangeState(DeadState);

        IdleState = new PlayerIdleState();
        RunState = new PlayerRunState();
        AttackState = new PlayerAttackState();
        JumpUpState = new PlayerJumpUpState();
        JumpDownState = new PlayerJumpDownState();
        HitState = new PlayerHitState();
        DeadState = new PlayerDeadState();
        StateMachine = new StateMachine<Player>(this);

        CheckComponentOnNull();
    }

    private void CheckComponentOnNull() {
        if (InvulnerableStatus == null) {
            Debug.LogError($"Component {nameof(InvulnerabilityStatus)} is null");
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
        _playerHealthView.UpdateHealthBar();
    }

    private void Update() {
        StateMachine.Update();
        HealthController.RegenerationHealth();
        ToggleInventory();
    }

    private void FixedUpdate() {
        StateMachine.FixedUpdate();
    }

    public void SetGravityScale(float value) {
        Rigidbody.gravityScale = value;
    }

    public void ResetGravityScaleToDefault() {
        Rigidbody.gravityScale = _deafaultGravityScale;
    }

    private void ToggleInventory() {
        if (PlayerMovement.IsOpenInventory) {
            Inventory.ActiveToggle();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out IInteracvite interacvite)) {
            print("interactive set");
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
