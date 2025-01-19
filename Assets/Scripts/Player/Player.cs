using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private IInteracvite _interactive;
    private PlayerHealthView _playerHealthView;

    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private List<Collider2D> _collidersForIgnored = new List<Collider2D>();
    [SerializeField]
    private float _delayBllinkAnimationInSeconds;
    [SerializeField]
    private List<SpriteRenderer> _sprites = new List<SpriteRenderer>();
    [SerializeField]
    public PlayerHealthController _healthController;

    public List<Collider2D> CollidesForIgnored { get => _collidersForIgnored; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerJumpUpState JumpUpState { get; private set; }
    public PlayerJumpDownState JumpDownState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }

    public IInteracvite Interactive { get => _interactive; }
    [field: SerializeField]
    public InvulnerabilityStatus InvulnerableStatus { get; private set; }
    [field: SerializeField]
    public PlayerMovement PlayerMovement { get; private set; }
    [field: SerializeField]
    public PlayerWeaponController PlayerWeaponController { get; private set; }
    [field: SerializeField]
    public AnimationController AnimationController { get; private set; }

    private void Awake() {
        PlayerWeaponController.Init();
        EventManager.OnHit += () => StateMachine.ChangeState(HitState);
        EventManager.OnHit += () => {
            StartCoroutine(InvulnerableStatus.ActivateInvulnerabilityStatus());
            StartCoroutine(BlinkAnimation());
        };
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

    private void OnDestroy() {
        EventManager.OnHit -= () => StateMachine.ChangeState(HitState);
        EventManager.OnHit -= () => {
            StartCoroutine(InvulnerableStatus.ActivateInvulnerabilityStatus());
            StartCoroutine(BlinkAnimation());
        };
        EventManager.OnDead -= () => StateMachine.ChangeState(DeadState);
    }

    private void CheckComponentOnNull() {
        if (InvulnerableStatus == null) {
            Debug.LogError($"Component {nameof(InvulnerabilityStatus)} is null");
        }
    }

    private void Start() {
        StateMachine.ChangeState(IdleState);
        _playerHealthView = FindAnyObjectByType<PlayerHealthView>();
        _playerHealthView.UpdateHealthBar();
    }

    private void Update() {
        StateMachine.Update();
        _healthController.RegenerationHealth();
        ToggleInventory();
    }

    private void FixedUpdate() {
        StateMachine.FixedUpdate();
    }

    private void ToggleInventory() {
        if (PlayerMovement.IsPressedOpenInventoryButton) {
            _inventory.Open();
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

    public IEnumerator BlinkAnimation() {
        while (InvulnerableStatus.IsInvulnerability) {
            yield return new WaitForSeconds(_delayBllinkAnimationInSeconds);
            SetSpritesAlpha(0);
            yield return new WaitForSeconds(_delayBllinkAnimationInSeconds);
            SetSpritesAlpha(255);
        }
    }

    private void SetSpritesAlpha(float alpha) {
        foreach (var element in _sprites) {
            element.color = new Color(255f, 255f, 255f, alpha);
        }
    }
}
