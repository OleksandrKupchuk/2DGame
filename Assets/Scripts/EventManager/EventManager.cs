using System;

public class EventManager {
    public static event Action<WearableItemData> OnItemDressed;

    public static void OnItemDressedHandler(WearableItemData item) {
        OnItemDressed.Invoke(item);
    }

    public static void OnItemDressedHandler() {
        OnItemDressed.Invoke(null);
    }

    public static event Action<ItemData> TakeAwayItem;

    public static void TakeAwayItemEventHandler(ItemData item) {
        TakeAwayItem.Invoke(item);
    }

    public static event Action<UsableItemData> UseItem;

    public static void UseItemEventHandler(UsableItemData item) {
        UseItem.Invoke(item);
    }

    public static event Action<ItemData> ActionItemOver;

    public static void ActionItemOverEventHandler(ItemData item) {
        ActionItemOver.Invoke(item);
    }

    public static event Action<ItemData> BuyItem;

    public static void BuyItemEventHandler(ItemData item) {
        BuyItem.Invoke(item);
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
