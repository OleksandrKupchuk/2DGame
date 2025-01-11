using UnityEngine;
using UnityEngine.UI;

public class UsageSlotView : SlotView {
    [SerializeField]
    protected Image _border;

    private void Awake() {
        DragAndDrop.OnItemTaken += ChangeBorderColor;
        DragAndDrop.OnItemPutted += ResetBorderColor;
    }

    private void OnDestroy() {
        DragAndDrop.OnItemTaken -= ChangeBorderColor;
        DragAndDrop.OnItemPutted -= ResetBorderColor;
    }

    public override void PutItem(ItemData itemData) {
        if (CanUseItem(itemData)) {
            _itemData = itemData;
            SetIcon();
        }
        else {
            _itemData = null;
            SetIcon();
        }
    }

    public override void TakeItem() {
        _itemData = null;
        SetIcon();
    }

    private bool CanUseItem(ItemData itemData) {
        if (itemData is UsableItemData) {
            return true;
        }

        return false;
    }

    private void ChangeBorderColor(ItemData itemData) {
        if (CanUseItem(itemData)) {
            SetBorderColor(Color.green);
        }
        else {
            SetBorderColor(Color.red);
        }
    }

    private void ResetBorderColor() {
        SetBorderColor(Color.white);
    }

    private void SetBorderColor(Color color) {
        _border.color = color;
    }
}
