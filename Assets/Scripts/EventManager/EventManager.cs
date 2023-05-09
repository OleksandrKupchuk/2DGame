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

    public static event Action TookOffItem;

    public static void TookOffItemEventHandler() {
        TookOffItem.Invoke();
    }
}