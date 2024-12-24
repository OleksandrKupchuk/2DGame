using UnityEngine.InputSystem;

public class PlayerInput {
    private PlayerHandleAction _playerHandleAction;
    private const string _playerMap = "Player";

    public InputAction Jump { get; private set; }
    public InputAction Move { get; private set; }
    public InputAction Attack { get; private set; }
    public InputAction HandleInventoryInputAction { get; private set; }
    public InputAction Interaction { get; private set; }

    public void Init(InputActionAsset inputActionAsset) {
        _playerHandleAction = new PlayerHandleAction(inputActionAsset);
        _playerHandleAction.FindMap(_playerMap);
        Jump = _playerHandleAction.GetAction("Jump");
        Move = _playerHandleAction.GetAction("Movement");
        Attack = _playerHandleAction.GetAction("Attack");
        HandleInventoryInputAction = _playerHandleAction.GetAction("HandleInventory");
        Interaction = _playerHandleAction.GetAction("Interactive");
    }
}
