using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour {
    private List<InventoryCellView> _cells = new List<InventoryCellView>();

    [SerializeField]
    private InventoryCellView _inventoryCellPrefab;
    [SerializeField]
    private InventoryController _inventoryController;
    [SerializeField]
    private Transform _parentCell;
    [SerializeField]
    private GameObject _background;
    [SerializeField]
    private Button _closeButton;

    private void Awake() {
        SpawnCellView();
        _inventoryController.OnAddItem += AddItem;
    }

    private void OnDestroy() {
        _inventoryController.OnAddItem -= AddItem;
    }

    private void SpawnCellView() {
        for (int i = 0; i < _inventoryController.AmountItems; i++) {
            InventoryCellView _cell = Instantiate(_inventoryCellPrefab, _parentCell);
            _cell.gameObject.name = _cell.gameObject.name + " " + i;
            _cell.PutItem(null);
            _cells.Add(_cell);
        }
    }

    private void AddItem(ItemData itemData) {
        foreach (InventoryCellView _cell in _cells) {
            if(_cell.IsEmpty) {
                _cell.PutItem(itemData);
                return;
            }
        }
    }
}
