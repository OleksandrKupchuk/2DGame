using System;
using UnityEngine;

public class EventManager : MonoBehaviour {
    public static event Action PutOrTakeAwakeItem;

    public static void PutOrTakeAwayItemInPlayerSlot() {
        PutOrTakeAwakeItem.Invoke();
    }
}