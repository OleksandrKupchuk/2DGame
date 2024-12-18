using UnityEngine.InputSystem;

public class PlayerInput {
    private PlayerHandleAction _playerHandleAction;
    private const string _playerMap = "Player";

    public InputAction JumpInputAction { get; private set; }
    public InputAction MovementInputAction { get; private set; }
    public InputAction AttackInputAction { get; private set; }
    public InputAction HandleInventoryInputAction { get; private set; }
    public InputAction InteractiveInputAction { get; private set; }

    public void Init(InputActionAsset inputActionAsset) {
        _playerHandleAction = new PlayerHandleAction(inputActionAsset);
        _playerHandleAction.FindMap(_playerMap);
        JumpInputAction = _playerHandleAction.GetAction("Jump");
        MovementInputAction = _playerHandleAction.GetAction("Movement");
        AttackInputAction = _playerHandleAction.GetAction("Attack");
        HandleInventoryInputAction = _playerHandleAction.GetAction("HandleInventory");
        InteractiveInputAction = _playerHandleAction.GetAction("Interactive");
    }
}
