using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private InputActionReference _openCloseInventoryInputAction;
    [SerializeField]
    private Inventory _inventory;

    private void Update() {
        CheckOpenCloseInventory();
    }

    private void CheckOpenCloseInventory() {
        if (_openCloseInventoryInputAction.action.triggered) {
            //print("click I");
            EventManager.InventoryOpenlyClosedEventHandler();
        }
    }
}
