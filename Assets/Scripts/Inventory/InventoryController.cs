using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject {
    private List<Item> _itemsData = new List<Item>();

    [field: SerializeField]
    public int AmountItems { get; private set; }

    public event Action<Item> OnAddItem;
    public event Action<Item> OnRemoveItem;

    public bool IsEmptySlot => AmountItems > _itemsData.Count;

    private void OnEnable() {
        _itemsData.Clear();
    }

    public bool TryAddItem(Item itemData) {
        if(!IsEmptySlot) {
            Debug.Log("In Inventory there is not a place");
            return false;
        }
        if(_itemsData.Contains(itemData)) {
            Debug.Log("Item already add");
            return false;
        }

        _itemsData.Add(itemData);
        OnAddItem?.Invoke(itemData);

        return true;
    }

    public void RemoveItem(Item itemData) {
        if(!_itemsData.Contains(itemData)) {
            Debug.Log("Can`t remove item because it not was added");
            return;
        }

        _itemsData.Remove(itemData);
        OnRemoveItem?.Invoke(itemData);
    }

    public void OpenInventory() {
        Debug.Log("Open inventory");
    }
}
