using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private Vector2 _movementInput;
    private float _health;
    private Damage _damageHit = null;
    private List<Damage> _objectsAttck = new List<Damage>();

    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _jumpVelocity;
    [SerializeField]
    private InputActionReference _movementInputAction;
    [SerializeField]
    private InputActionReference _shotInputAction;
    [SerializeField]
    private InputActionReference _jumpInputAction;
    [SerializeField]
    private CheckGround _checkGround;
    [SerializeField]
    private InvulnerabilityAnimation _invulnerableStatus;

    public bool isAttack = false;
    public bool isJump = false;
    public bool isHit = false;

    public bool IsFalling { get => Rigidbody.velocity.y < 0.001; }
    public bool IsDead { get => _health <= 0; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerJumpUpState JumpUpState { get; private set; }
    public PlayerJumpDownState JumpDownState { get; private set; }
    public PlayerHitState HitState { get; private set; }
    public PlayerDeadState DeadState { get; private set; }

    public Rigidbody2D Rigidbody { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }
    public Vector2 MovementInput { get; }
    public Animator Animator { get; private set; }
    public InputActionReference ShotInputAction { get => _shotInputAction; }
    public InputActionReference JumpInputAction { get => _jumpInputAction; }
    public CheckGround CheckGround { get => _checkGround; }
    public InvulnerabilityAnimation InvulnerableStatus { get =>_invulnerableStatus; }

    private void OnEnable() {

    }

    private void OnDisable() {

    }

    private void Awake() {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
        IdleState = new PlayerIdleState();
        RunState = new PlayerRunState();
        AttackState = new PlayerAttackState();
        JumpUpState = new PlayerJumpUpState();
        JumpDownState = new PlayerJumpDownState();
        HitState = new PlayerHitState();
        DeadState = new PlayerDeadState();
        StateMachine = new StateMachine<Player>(this);
        CheckComponentOnNull();
        _health = _maxHealth;
    }

    private void CheckComponentOnNull() {
        if (_movementInputAction == null) {
            Debug.LogError("Component MovementInputAction is null");
        }
        if (_shotInputAction == null) {
            Debug.LogError("Component ShotInputAction is null");
        }
        if (_jumpInputAction == null) {
            Debug.LogError("Component JumpInputAction is null");
        }
        if (_checkGround == null) {
            Debug.LogError("Component CheckGround is null");
        }
        if (_invulnerableStatus == null) {
            Debug.LogError("Component InvulnerableStatus is null");
        }
    }

    private void Start() {
        StateMachine.ChangeState(IdleState);
    }

    private void Update() {
        StateMachine.Update();
    }

    private void FixedUpdate() {
        StateMachine.FixedUpdate();
    }

    public void Move() {
        Rigidbody.velocity = new Vector2(_movementInput.x * _speed, Rigidbody.velocity.y);
    }

    public Vector2 GetMovementInput() {
        return _movementInput = _movementInputAction.action.ReadValue<Vector2>();
    }

    public void Flip() {
        if (_movementInput.x > 0) {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_movementInput.x < 0) {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetAttackBoolTrue(InputAction.CallbackContext obj) {
        isAttack = true;
    }

    public void SetJumpBoolTrue(InputAction.CallbackContext obj) {
        isJump = true;
    }

    public void ResetRigidbodyVelocity() {
        Rigidbody.velocity = Vector2.zero;
    }

    public bool IsEndCurrentAnimation(int layer) {
        if (Animator.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1) {
            return true;
        }

        return false;
    }

    public void Jump() {
        Rigidbody.velocity = Vector2.up * _jumpVelocity;
    }

    public void CheckTakeDamage(float damage, Damage damageObject) {

        //if (damageObject == _damageHit) {
        //    StartCoroutine(ResetAllHit());
        //    return;
        //}

        if (IsThisAlreadyAttacked(damageObject)) {
            ResetCurrentDamage(damageObject);
            return;
        }

        _objectsAttck.Add(damageObject);
        //_damageHit = damageObject;
        TakeDamage(damage);
    }

    private bool IsThisAlreadyAttacked(Damage damageObject) {
        if(_objectsAttck.Count == 0) {
            return false;
        }

        foreach (var attack in _objectsAttck) {
            if(attack == damageObject) {
                //print("this object already attacked");
                return true;
            }
        }

        return false;
    }

    private void TakeDamage(float damage) {
        _health -= damage;
        //print("health = " + _health);
        if (IsDead) {
            StateMachine.ChangeState(DeadState);
        }
        else {
            isHit = true;
        }
    }

    private IEnumerator ResetAllHit() {
        yield return new WaitForSeconds(1);
        _damageHit = null;
    }

    private IEnumerator ResetCurrentDamage(Damage damageObject) {
        yield return new WaitForSeconds(1);
        _objectsAttck.Remove(damageObject);
    }
}
