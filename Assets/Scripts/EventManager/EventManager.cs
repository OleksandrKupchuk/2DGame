using System;

public class EventManager {
    public static event Action<WearableItem> OnItemDressed;

    public static void OnItemDressedHandler(WearableItem item) {
        OnItemDressed.Invoke(item);
    }

    public static void OnItemDressedHandler() {
        OnItemDressed.Invoke(null);
    }

    public static event Action<Item> TakeAwayItem;

    public static void TakeAwayItemEventHandler(Item item) {
        TakeAwayItem.Invoke(item);
    }

    public static event Action<UsableItem> UseItem;

    public static void UseItemEventHandler(UsableItem item) {
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

    public static event Action<AttributeType> OnAttributeChanged;

    public static void OnAttributeChangedHandler(AttributeType attributeType) {
        OnAttributeChanged.Invoke(attributeType);
    }
}
