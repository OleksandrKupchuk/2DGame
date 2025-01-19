using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private RaycastHit2D _raycastHit;
    private Vector2 _inputDirection;
    private float _deafaultGravityScale;

    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private PlayerInputReader _playerInputReader;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private float _distanceRaycastHit;
    [SerializeField]
    private LayerMask _groundLayer;

    public bool IsLookingLeft { get => transform.localScale.x > 0; }
    public bool IsFalling { get => _rigidbody.velocity.y < 0; }
    public bool IsMove { get => Math.Abs(_inputDirection.x) > 0; }
    public bool IsJump {
        get {
            if (_playerInputReader.Jump.triggered && IsGround()) {
                return true;
            }

            return false;
        }
    }
    public bool IsAttack {
        get {
            if (_playerInputReader.Attack.triggered) {
                return true;
            }

            return false;
        }
    }
    public bool IsPressedOpenInventoryButton { get => _playerInputReader.HandleInventoryInputAction.triggered; }
    public bool IsInteraction { get => _playerInputReader.Interaction.triggered; }

    private void Awake() {
        _playerInputReader.OnMoved += Move;
        _deafaultGravityScale = _rigidbody.gravityScale;
    }

    private void OnDestroy() {
        _playerInputReader.OnMoved -= Move;
    }

    private void Update() {
        IsGround();
    }

    private void Move(Vector2 direction) {
        _inputDirection = direction;
    }

    public bool IsGround() {
        Color _color;
        _raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0f, Vector2.down, _distanceRaycastHit, _groundLayer);

        _color = _raycastHit.transform != null ? Color.green : Color.red;

        Debug.DrawRay(_boxCollider.bounds.center + new Vector3(_boxCollider.bounds.extents.x, 0), Vector3.down * (_boxCollider.bounds.extents.y + _distanceRaycastHit), _color);
        Debug.DrawRay(_boxCollider.bounds.center - new Vector3(_boxCollider.bounds.extents.x, 0), Vector3.down * (_boxCollider.bounds.extents.y + _distanceRaycastHit), _color);
        Debug.DrawRay(_boxCollider.bounds.center - new Vector3(_boxCollider.bounds.extents.x, _boxCollider.bounds.extents.y + _distanceRaycastHit), Vector3.right * _boxCollider.bounds.size.x, _color);
        
        return _raycastHit.transform != null;
    }

    public void Jump() {
        _rigidbody.velocity = Vector2.up * _playerConfig.JumpVelocity;
    }

    public void Run() {
        _rigidbody.velocity = new Vector2(_inputDirection.x * _playerConfig.Speed, _rigidbody.velocity.y);
    }

    public void Flip() {
        if (_inputDirection.x > 0) {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_inputDirection.x < 0) {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void SetGravityScale(float value) {
        _rigidbody.gravityScale = value;
    }

    public void ResetGravityScaleToDefault() {
        _rigidbody.gravityScale = _deafaultGravityScale;
    }

    public void ResetRigidbodyVelocity() {
        _rigidbody.velocity = Vector2.zero;
    }
}
