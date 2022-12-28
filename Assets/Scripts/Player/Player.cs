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
    private InputActionReference _movementInputAction;
    [SerializeField]
    private InputActionReference _shotInputAction;

    public bool isAttack = false;

    public Rigidbody2D Rigidbody { get; private set; }
    public StateMachine<Player> StateMachine { get; private set; }
    public Vector2 MovementInput { get; }
    public Animator Animator { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    public PlayerAttackState AttackState { get; private set; }
    public InputActionReference ShotInputAction { get => _shotInputAction; }

    private void Awake() {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
        IdleState = new PlayerIdleState();
        RunState = new PlayerRunState();
        AttackState = new PlayerAttackState();
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
        //if (isAttack) {
        //    return _movementInput = Vector2.zero;
        //}
        return _movementInput = _movementInputAction.action.ReadValue<Vector2>();
    }

    public void Flip() {
        //if (isAttack) {
        //    return;
        //}
        if (_movementInput.x > 0) {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_movementInput.x < 0) {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnDisable() {
        ShotInputAction.action.performed -= SetAttackBoolTrue;
    }

    public void SetAttackBoolTrue(InputAction.CallbackContext obj) {
        isAttack = true;
    }

    public void ResetRigidbodyVelocity() {
        Rigidbody.velocity = Vector2.zero;
    }
}
