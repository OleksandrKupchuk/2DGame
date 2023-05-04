using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    [SerializeField]
    private InputActionReference _openCloseInventoryInputAction;
    [SerializeField]
    private Inventory _inventory;

    public static event Action OpenCloseInventory;

    //private void OnEnable() {
    //    OpenCloseInventory += _inventory.EnableDisableInventory;
    //}

    private void Update() {
        CheckOpenCloseInventory();
    }

    private void CheckOpenCloseInventory() {
        if (_openCloseInventoryInputAction.action.triggered) {
            //print("click I");
            OpenCloseInventory.Invoke();
        }
    }

    //private void OnDestroy() {
    //    OpenCloseInventory -= _inventory.EnableDisableInventory;
    //}
}