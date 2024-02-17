using System.Collections.Generic;
using UnityEngine;

public class PlayerSlot : Cell {
    [SerializeField]
    private List<ItemType> _itemTypes = new List<ItemType>();


    private new void Awake() {
        base.Awake();
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
            return;
        }
        if (!_itemTypes.Contains(_equipment.ItemType)) {
            SetColorForBorder(Color.red);
        }
        else {
            SetColorForBorder(Color.green);
        }
    }

    private void ResetColorBorderCell() {
        SetColorForBorder(Color.white);
    }

    public override void SetItem(Item item) {
        Equipment _equipment = item as Equipment;

        if (_equipment != null && _itemTypes.Contains(_equipment.ItemType)) {
            Item = item;
            SetIcon(item.Icon);
            EnableIcon();
            EventManager.PutOnItemEventHandler(item);
        }
    }

    public override void RemoveItem() {
        EventManager.TakeAwayItemEventHandler(Item);
        Item = null;
        DisableIcon();
    }

    private void SetColorForBorder(Color color) {
        _border.color = color;
    }
}
