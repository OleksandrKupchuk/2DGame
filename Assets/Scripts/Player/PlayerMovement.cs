using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    private Config _playerConfig;
    private RaycastHit2D _raycastHit;
    private PlayerInput _playerInput;

    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private float _distanceRaycastHit;
    [SerializeField]
    private LayerMask _groundLayer;
    [SerializeField]
    private InputActionAsset _inputActionAsset;

    public bool IsLookingLeft { get => transform.localScale.x > 0; }
    public bool IsFalling { get => _rigidbody.velocity.y < 0; }
    public bool IsJump {
        get {
            if (_playerInput.Jump.triggered && IsGround()) {
                return true;
            }

            return false;
        }
    }
    public bool IsAttack {
        get {
            if (_playerInput.Attack.triggered) {
                return true;
            }
            return false;
        }
    }
    public bool IsOpenInventory { get => _playerInput.HandleInventoryInputAction.triggered; }
    public bool IsInteraction { get => _playerInput.Interaction.triggered; }

    public void Init(Config playerConfig) {
        _playerInput = new PlayerInput();
        _playerInput.Init(_inputActionAsset);
        _playerConfig = playerConfig;
    }

    private void Update() {
        IsGround();
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
        _rigidbody.velocity = Vector2.up * _playerConfig.jumpVelocity;
    }

    public void Run(float inputDirection) {
        _rigidbody.velocity = new Vector2(inputDirection * _playerConfig.speed, _rigidbody.velocity.y);
    }

    public void Flip() {
        if (GetMoveInput().x > 0) {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (GetMoveInput().x < 0) {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public Vector2 GetMoveInput() {
        return _playerInput.Move.ReadValue<Vector2>();
    }
}
