using System.Collections.Generic;
using UnityEngine;

public class MarketView : MonoBehaviour {
    private Dictionary<ItemData, MarketSlotView> _dictionaryItems = new();
    private List<MarketSlotView> _slots = new List<MarketSlotView>();
    private int _commission;
    private int _bufferComission;

    [SerializeField]
    private Market _market;
    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private Inventory _inventoryController;
    [SerializeField]
    private bool _isDiscount;
    [SerializeField]
    private GameObject _discount;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private MarketSlotView _slotView;
    [SerializeField]
    private Transform _content;

    private void Awake() {
        GenerateCartItems();
        _market.OnOpen += Open;
        _market.OnClose += Close;
        _market.OnRemoveItem += RemoveItem;
    }

    private void nDestroy() {
        _market.OnOpen -= Open;
        _market.OnClose -= Close;
        _market.OnRemoveItem -= RemoveItem;
    }

    private void GenerateCartItems() {
        for (int i = 0; i < _market.RandomItemsData.Count; i++) {
            MarketSlotView _slotViewObject = Instantiate(_slotView, _content);
            _slotViewObject.PutItem(_market.RandomItemsData[i]);
            _slots.Add(_slotViewObject);
        }
    }

    private void ShowDiscount() {
        if (_isDiscount) {
            _bufferComission = _commission;
            _commission = 0;
            _discount.SetActive(true);
            Invoke(nameof(DelayDiscount), 10f);
        }
        else {
            _commission = _bufferComission;
            _discount.SetActive(false);
        }

        UpdatePrice();
    }

    private void RemoveItem(ItemData itemData) {
        foreach (var slot in _slots) {
            if(slot.ItemData == itemData) {
                slot.TakeItem();
                return;
            }
        }
    }

    private void DelayDiscount() {
        _isDiscount = false;
    }

    private void UpdatePrice() {
        foreach (KeyValuePair<ItemData, MarketSlotView> item in _dictionaryItems) {
        }
    }

    public void Open() {
        _background.SetActive(true);
        ShowDiscount();
    }

    public void Close() {
        _background.SetActive(false);
    }
}
