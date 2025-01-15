using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerInput")]
public class PlayerInput: ScriptableObject {
    private const string _playerMap = "Player";
    private const string _marketMap = "Market";

    [SerializeField]
    private PlayerHandleAction _playerHandleAction;
    [SerializeField]
    private InputActionAsset _inputActionAsset;

    public InputAction Jump { get; private set; }
    public InputAction Move { get; private set; }
    public InputAction Attack { get; private set; }
    public InputAction HandleInventoryInputAction { get; private set; }
    public InputAction Interaction { get; private set; }
    public InputActionMap InputActionMap { get; private set; }
    public InputAction Buy { get; private set; }

    private void OnEnable() {
        InputActionMap = _inputActionAsset.FindActionMap(_playerMap);
        Jump = _playerHandleAction.FindMapAndGetAction(_playerMap, "Jump");
        Move = _playerHandleAction.FindMapAndGetAction(_playerMap, "Movement");
        Attack = _playerHandleAction.FindMapAndGetAction(_playerMap, "Attack");
        HandleInventoryInputAction = _playerHandleAction.FindMapAndGetAction(_playerMap, "HandleInventory");
        Interaction = _playerHandleAction.FindMapAndGetAction(_playerMap, "Interactive");

        Buy = _playerHandleAction.FindMapAndGetAction(_marketMap, "Buy");
    }
}
