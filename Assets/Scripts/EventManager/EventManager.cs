using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
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

    public static event Action<Item> PutOnItem;

    public static void PutOnItemEventHandler(Item item) {
        PutOnItem.Invoke(item);
    }

    public static event Action<Item> TakeAwayItem;

    public static void TakeAwayItemEventHandler(Item item) {
        TakeAwayItem.Invoke(item);
    }

    public static event Action<Potion> UsePotion;

    public static void UsePotionEventHandler(Potion potion) {
        UsePotion.Invoke(potion);
    }
}