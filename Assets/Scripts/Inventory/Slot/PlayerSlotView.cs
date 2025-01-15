using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlotView : SlotView {
    [SerializeField]
    protected Image _border;
    [SerializeField]
    private List<ItemType> _slotTypes = new List<ItemType>();

    private void Awake() {
        SetIcon();
        DragAndDrop.OnItemTaken += ChangeBorderColor;
        DragAndDrop.OnItemPutted += ResetBorderColor;
    }

    private void OnDestroy() {
        DragAndDrop.OnItemTaken -= ChangeBorderColor;
        DragAndDrop.OnItemPutted -= ResetBorderColor;
    }

    public override void PutItem(ItemData itemData) {
        if (itemData is WearableItemData) {
            var _item = itemData as WearableItemData;

            if (_slotTypes.Contains(_item.ItemType)) {
                _itemData = _item;
                SetIcon();
                EventManager.OnItemDressedHandler(_item);
            }
        }
        else {
            _itemData = null;
            SetIcon();
        }
    }

    public override void TakeItem() {
        EventManager.TakeAwayItemEventHandler(_itemData);
        _itemData = null;
        SetIcon();
    }

    private void ChangeBorderColor(ItemData item) {
        if (item is WearableItemData) {
            var _item = item as WearableItemData;

            if (_slotTypes.Contains(_item.ItemType)) {
                SetBorderColor(Color.green);
            }
            else {
                SetBorderColor(Color.red);
            }
        }

    }

    private void ResetBorderColor() {
        SetBorderColor(Color.white);
    }

    private void SetBorderColor(Color color) {
        _border.color = color;
    }
}
