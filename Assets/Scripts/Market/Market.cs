using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Market")]
public class Market : ScriptableObject {
    private List<ItemData> _randomItemsData;
    private int _commission;

    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private List<ItemData> _items;

    [field: SerializeField]
    public int AmountSlots { get; private set; }
    public List<ItemData> RandomItemsData => _items;
    public event Action<ItemData> OnAddItem;
    public event Action<ItemData> OnRemoveItem;
    public event Action OnOpen;
    public event Action OnClose;

    private void OnEnable() {
        //GetRandomItems();
    }

    private int GetRandomItems() {
        int _range = 0;

        if (_items.Count <= 1) {
            _range = 1;
        }
        else {
            _range = _items.Count;
        }

        //int _range = Random.Range(1, _items.Count);

        for (int i = 0; i < _range; i++) {
            _randomItemsData.Add(_items[i]);
        }

        return _range;
    }

    public void BuyItem(ItemData item) {
        int _itemPrice = GetPriceWithTraderComission(item.Price);

        if (_playerConfig.coins >= _itemPrice) {
            _playerConfig.coins -= _itemPrice;
            _inventory.TryAddItem(item);
        }
        else {
            Debug.LogWarning("Not enough money");
        }
    }

    private int GetPriceWithTraderComission(int itemPrice) {
        return itemPrice + (itemPrice * _commission / 100);
    }

    public void SetCommission(int commission) {
        _commission = commission;
    }

    public void Open() {
        OnOpen?.Invoke();
    }

    public void Close() {
        OnClose?.Invoke();
    }

    public void AddItem(ItemData item) {
        _randomItemsData.Add(item);
        OnAddItem?.Invoke(item);
    }

    public void RemoveItem(ItemData item) {
        _randomItemsData.Remove(item);
        OnRemoveItem?.Invoke(item);
    }
}
