using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "PlayerHandleAction")]
public class PlayerHandleAction: ScriptableObject {
    [SerializeField]
    private InputActionAsset _inputActionAsset;

    public InputAction FindMapAndGetAction(string mapName, string actionName) {
        InputActionMap _inputActionMap = _inputActionAsset.FindActionMap(mapName);

        if(_inputActionMap == null) {
            Debug.LogError($"Not found action map {mapName}");
        }

        if (actionName.Equals("") || actionName.Equals(null)) {
            Debug.LogError("Action name is empty or null");
        }

        return _inputActionMap.FindAction(actionName);
    }
}
