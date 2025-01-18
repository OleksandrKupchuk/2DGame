using System.Collections.Generic;
using UnityEngine;

public class MarketView : MonoBehaviour {
    private List<MarketSlotView> _slots = new List<MarketSlotView>();

    [SerializeField]
    private Market _market;
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

        Close();
    }

    private void OnDestroy() {
        _market.OnOpen -= Open;
        _market.OnClose -= Close;
        _market.OnRemoveItem -= RemoveItem;
    }

    private void GenerateCartItems() {
        for (int i = 0; i < _market.AmountSlots; i++) {
            MarketSlotView _slotViewObject = Instantiate(_slotView, _content);

            if (i < _market.RandomItemsData.Count) {
                _slotViewObject.PutItem(_market.RandomItemsData[i]);
            }
            else {
                _slotViewObject.PutItem(null);
            }

            _slots.Add(_slotViewObject);
        }
    }

    private void AddItem(ItemData itemData) {
        foreach (var slot in _slots) {
            if (slot.IsEmpty) {
                slot.PutItem(itemData);
                return;
            }
        }
    }

    private void RemoveItem(ItemData itemData) {
        foreach (var slot in _slots) {
            if (slot.ItemData == itemData) {
                slot.TakeItem();
                return;
            }
        }
    }

    public void Open() {
        _background.SetActive(true);
    }

    public void Close() {
        _background.SetActive(false);
    }
}
