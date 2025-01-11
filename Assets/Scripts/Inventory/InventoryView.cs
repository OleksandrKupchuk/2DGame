using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour {
    private List<InventorySlotView> _slots = new List<InventorySlotView>();

    [SerializeField]
    private InventorySlotView _slotViewPrefab;
    [SerializeField]
    private Inventory _inventoryController;
    [SerializeField]
    private Transform _bag;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private Button _closeButton;

    private void Awake() {
        SpawnCellView();
        _inventoryController.OnAddItem += AddItem;
        _inventoryController.OnRemoveItem += RemoveItem;
    }

    private void OnDestroy() {
        _inventoryController.OnAddItem -= AddItem;
        _inventoryController.OnRemoveItem -= RemoveItem;
    }

    private void SpawnCellView() {
        for (int i = 0; i < _inventoryController.AmountItems; i++) {
            InventorySlotView _cell = Instantiate(_slotViewPrefab, _bag);
            _cell.gameObject.name = _cell.gameObject.name + " " + i;
            _cell.PutItem(null);
            _slots.Add(_cell);
        }
    }

    private void AddItem(Item itemData) {
        foreach (var _slot in _slots) {
            if(_slot.IsEmpty) {
                _slot.PutItem(itemData);
                return;
            }
        }
    }

    private void RemoveItem(Item itemData) {
        foreach (var _slot in _slots) {
            if (!_slot.IsEmpty && _slot.ItemData == itemData) {
                _slot.TakeItem();
                return;
            }
        }
    }
}
