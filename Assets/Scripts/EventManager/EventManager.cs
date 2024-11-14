using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static event Action InventoryOpenlyClosed;

    public static void InventoryOpenlyClosedEventHandler() {
        InventoryOpenlyClosed.Invoke();
    }

    public static event Action<WearableItem> PutOnItem;

    public static void PutOnItemEventHandler(WearableItem item) {
        PutOnItem.Invoke(item);
    }

    public static event Action<Item> TakeAwayItem;

    public static void TakeAwayItemEventHandler(Item item) {
        TakeAwayItem.Invoke(item);
    }

    public static event Action<ItemUsable> UseItem;

    public static void UseItemEventHandler(ItemUsable item) {
        UseItem.Invoke(item);
    }

    public static event Action<Item> ActionItemOver;

    public static void ActionItemOverEventHandler(Item item) {
        ActionItemOver.Invoke(item);
    }
}
