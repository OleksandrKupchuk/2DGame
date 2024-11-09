using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static event Action InventoryOpenlyClosed;

    public static void InventoryOpenlyClosedEventHandler() {
        InventoryOpenlyClosed.Invoke();
    }

    public static event Action UpdateAttributes;

    public static void UpdateAttributesEventHandler() {
        UpdateAttributes.Invoke();
    }

    public static event Action<Item> PutOnItem;

    public static void PutOnItemEventHandler(Item item) {
        PutOnItem.Invoke(item);
    }

    public static event Action<Item> TakeAwayItem;

    public static void TakeAwayItemEventHandler(Item item) {
        TakeAwayItem.Invoke(item);
    }

    public static event Action<Item> UseItem;

    public static void UseItemEventHandler(Item item) {
        UseItem.Invoke(item);
    }
}
