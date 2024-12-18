using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandleAction {
    private InputActionAsset _inputActionAsset;
    private InputActionMap _inputActionMap;

    public PlayerHandleAction(InputActionAsset inputActionAsset) {
        _inputActionAsset = inputActionAsset;
    }

    public void FindMap(string mapName) {
        _inputActionMap = _inputActionAsset.FindActionMap(mapName);

        if(_inputActionMap == null) {
            Debug.LogError($"Not found action map {mapName}");
        }
    }

    public InputAction GetAction(string actionName) {
        if(actionName.Equals("") || actionName.Equals(null)) {
            Debug.LogError("Action name is empty or null");
        }

        return _inputActionMap.FindAction(actionName);
    }
}
