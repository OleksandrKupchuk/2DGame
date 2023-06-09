using System.Collections.Generic;
using UnityEngine;

public class PlayerSlot : MonoBehaviour {
    private Cell _cell;
    [SerializeField]
    private List<ItemType> _itemTypes = new List<ItemType>();

    public Cell Cell { get => _cell; }

    private void Awake() {
        _cell = GetComponent<Cell>();
        DragDropController.RaisedItemTrigger += ChageColorBorderCell;
        DragDropController.DropPutItemTrigger += ResetColorBorderCell;
    }

    private void OnDestroy() {
        DragDropController.RaisedItemTrigger -= ChageColorBorderCell;
        DragDropController.DropPutItemTrigger -= ResetColorBorderCell;
    }

    private void ChageColorBorderCell(Item item) {
        Equipment _equipment = item as Equipment;
        if (_equipment == null) {
            print("Item not Equipment");
            return;
        }
        if (!_itemTypes.Contains(_equipment.ItemType)) {
            _cell.SetAvailableForInteraction(false);
            _cell.SetRedBorder();
        }
        else {
            _cell.SetGreenBorder();
        }
    }

    private void ResetColorBorderCell() {
        _cell.SetAvailableForInteraction(true);
        _cell.EnableBoxCollider();
        _cell.ResetColorBorder();
    }
}