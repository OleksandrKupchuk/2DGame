using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static event Action PutOnOrTakenAwakeItem;

    public static void PutOnOrTakenAwakeItemEventHandler() {
        PutOnOrTakenAwakeItem.Invoke();
    }

    public static event Action InventoryOpenlyClosed;

    public static void InventoryOpenlyClosedEventHandler() {
        InventoryOpenlyClosed.Invoke();
    }

    public static event Action UpdatingHealthBar;

    public static void UpdatingHealthBarEventHandler() {
        UpdatingHealthBar.Invoke();
    }

    public static event Action UpdatePlayerCurrentHealth;

    public static void UpdatePlayerCurrentHealthEventHandler() {
        UpdatePlayerCurrentHealth.Invoke();
    }

    public static event Action<Equipment> PutOnItem;

    public static void PutOnItemEventHandler(Item item) {
        Equipment _equipment = item as Equipment;
        if (_equipment == null) {
            Debug.Log("Equipment is null");
            return;
        }
        PutOnItem.Invoke(_equipment);
    }

    public static event Action<Equipment> TakeAwayItem;

    public static void TakeAwayItemEventHandler(Item item) {
        Equipment _equipment = item as Equipment;
        if (_equipment == null) {
            Debug.Log("Equipment is null");
            return;
        }
        TakeAwayItem.Invoke(_equipment);
    }
}