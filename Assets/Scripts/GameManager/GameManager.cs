using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private InputActionReference _openCloseInventoryInputAction;

    private void Update() {
        //CheckOpenCloseInventory();
    }

    private void CheckOpenCloseInventory() {
        if (_openCloseInventoryInputAction.action.triggered) {
            //print("click I");
            //EventManager.InventoryOpenlyClosedEventHandler();
        }
    }
}
