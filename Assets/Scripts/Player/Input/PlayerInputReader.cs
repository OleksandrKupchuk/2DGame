using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInputReader")]
public class PlayerInputReader : ScriptableObject {
    private const string _playerMap = "Player";
    private const string _marketMap = "Market";
    private InputActionMap _playerActionMap;
    private InputActionMap _marketActionMap;
    private InputAction _move;

    [SerializeField]
    private InputActionAsset _inputActionAsset;

    public InputAction Jump { get; private set; }
    public InputAction Attack { get; private set; }
    public InputAction HandleInventoryInputAction { get; private set; }
    public InputAction Interaction { get; private set; }
    public InputAction Buy { get; private set; }

    public event Action<Vector2> OnMoved;

    private void OnEnable() {
        _playerActionMap = _inputActionAsset.FindActionMap(_playerMap);
        Jump = _playerActionMap.FindAction("Jump");
        _move = _playerActionMap.FindAction("Movement");
        Attack = _playerActionMap.FindAction("Attack");
        HandleInventoryInputAction = _playerActionMap.FindAction("HandleInventory");
        Interaction = _playerActionMap.FindAction("Interact");

        _marketActionMap = _inputActionAsset.FindActionMap(_marketMap);
        Buy = _marketActionMap.FindAction("BuyItem");

        EnablePlayerInput();

        _move.started += OnMove;
        _move.performed += OnMove;
        _move.canceled += OnMove;
    }

    private void OnDisable() {
        _move.started -= OnMove;
        _move.performed -= OnMove;
        _move.canceled -= OnMove;

        DisablePlayerInput();
    }

    private void OnMove(InputAction.CallbackContext action) {
        OnMoved?.Invoke(action.ReadValue<Vector2>());
    }

    public void EnablePlayerInput() {
        _playerActionMap.Enable();
    }

    public void DisablePlayerInput() {
        _playerActionMap.Disable();
    }

    public void DisableMarketInput() {
        _marketActionMap.Disable();
    }

    public void EnableMarketInput() {
        _marketActionMap.Enable();
    }
}
