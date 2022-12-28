using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {
    private Vector2 _movementInput;

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

    public bool isAttack = false;
    public bool isJump = false;

    public Rigidbody2D Rigidbody { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }
    public Vector2 MovementInput { get; }
    public Animator Animator { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public PlayerJumpUpState JumpUpState { get; private set; }
    public InputActionReference ShotInputAction { get => _shotInputAction; }
    public InputActionReference JumpInputAction { get => _jumpInputAction; }
    public CheckGround CheckGround { get => _checkGround; }

    private void Awake() {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
        IdleState = new PlayerIdleState();
        RunState = new PlayerRunState();
        AttackState = new PlayerAttackState();
        JumpUpState = new PlayerJumpUpState();
        StateMachine = new StateMachine<Player>(this);
        CheckComponentOnNull();
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
        print("velocity " + Rigidbody.velocity);
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

    private void OnDisable() {
        ShotInputAction.action.performed -= SetAttackBoolTrue;
        ShotInputAction.action.performed -= SetJumpBoolTrue;
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
}
