using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject {
    private List<ItemData> _itemsData = new List<ItemData>();

    [field: SerializeField]
    public int AmountSlots { get; private set; }

    public event Action OnOpen;
    public event Action OnClose;
    public event Action<ItemData> OnAddItem;
    public event Action<ItemData> OnRemoveItem;

    public bool IsEmptySlot => AmountSlots > _itemsData.Count;

    private void OnEnable() {
        _itemsData.Clear();
    }

    public bool TryAddItem(ItemData itemData) {
        if(!IsEmptySlot) {
            Debug.Log("In Inventory there is not a place");
            return false;
        }
        if(_itemsData.Contains(itemData)) {
            Debug.Log("ItemData already add");
            return false;
        }

        _itemsData.Add(itemData);
        OnAddItem?.Invoke(itemData);

        return true;
    }

    public void RemoveItem(ItemData itemData) {
        if(!_itemsData.Contains(itemData)) {
            Debug.Log("Can`t remove item because it not was added");
            return;
        }

        _itemsData.Remove(itemData);
        OnRemoveItem?.Invoke(itemData);
    }

    public void Open() {
        OnOpen?.Invoke();
    }

    public void Close() {
        OnClose?.Invoke();
    }
}
