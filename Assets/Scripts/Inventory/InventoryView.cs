using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour {
    private List<InventorySlotView> _slots = new List<InventorySlotView>();

    [SerializeField]
    private Market _market;
    [SerializeField]
    private InventorySlotView _slotViewPrefab;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private Transform _bag;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private Button _closeButton;

    private void Awake() {
        SpawnSlotsView();
        _inventory.OnOpen += Open;
        _inventory.OnClose += Close;
        _inventory.OnAddItem += AddItem;
        _inventory.OnRemoveItem += RemoveItem;
        _closeButton.onClick.AddListener(() => { Close(); _market.Close(); });

        Close();
    }

    private void OnDestroy() {
        _inventory.OnOpen -= Open;
        _inventory.OnClose -= Close;
        _inventory.OnAddItem -= AddItem;
        _inventory.OnRemoveItem -= RemoveItem;
    }

    private void SpawnSlotsView() {
        for (int i = 0; i < _inventory.AmountSlots; i++) {
            InventorySlotView _cell = Instantiate(_slotViewPrefab, _bag);
            _cell.gameObject.name = _cell.gameObject.name + " " + i;
            _cell.PutItem(null);
            _slots.Add(_cell);
        }
    }

    private void AddItem(ItemData itemData) {
        foreach (var _slot in _slots) {
            if(_slot.IsEmpty) {
                _slot.PutItem(itemData);
                return;
            }
        }
    }

    private void RemoveItem(ItemData itemData) {
        foreach (var _slot in _slots) {
            if (!_slot.IsEmpty && _slot.ItemData == itemData) {
                _slot.TakeItem();
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
