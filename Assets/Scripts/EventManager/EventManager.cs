using System;

public class EventManager {
    public static event Action<WearableItem> OnItemDressed;

    public static void OnItemDressedHandler(WearableItem item) {
        OnItemDressed.Invoke(item);
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

    public static event Action<Item> BuyItem;

    public static void BuyItemEventHandler(Item item) {
        BuyItem.Invoke(item);
    }

    public static event Action CloseInventory;

    public static void CloseInventoryEventHandler() {
        CloseInventory.Invoke();
    }

    public static event Action OnHealthChanged;

    public static void OnHealthChangedHandler() {
        OnHealthChanged.Invoke();
    }

    public static event Action OnDead;
    public static void OnDeadHandler() {
        OnDead.Invoke();
    }

    public static event Action OnHit;
    public static void OnHitHandler() {
        OnHit.Invoke();
    }
}
