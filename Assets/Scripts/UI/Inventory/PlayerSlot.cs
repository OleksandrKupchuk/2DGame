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
            SetAvailableForInteraction(false);
            SetRedBorder();
        }
        else {
            SetGreenBorder();
        }
    }

    private void ResetColorBorderCell() {
        SetAvailableForInteraction(true);
        //EnableBoxCollider();
        ResetColorBorder();
    }

    public override bool IsCanPut(Item item) {
        Equipment _equipment = item as Equipment;

        if (_equipment == null) {
            return false;
        }

        return true;
    }
}