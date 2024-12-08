using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHandleAction : MonoBehaviour {
    private InputActionMap _playerMap;

    [SerializeField]
    private InputActionAsset _inputActionAsset;

    private void Awake() {
        FindPlayerMap();
    }

    public void FindPlayerMap() {
        _playerMap = _inputActionAsset.FindActionMap("Player");

        if(_playerMap == null) {
            Debug.LogError("Not found action map 'Player'");
        }
    }

    public InputAction GetAction(string actionName) {
        if(actionName.Equals("") || actionName.Equals(null)) {
            Debug.LogError("Action name is empty or null");
        }

        return _playerMap.FindAction(actionName);
    }
}
