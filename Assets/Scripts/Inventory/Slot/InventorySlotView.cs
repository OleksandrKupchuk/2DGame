public class InventorySlotView : SlotView {
    public override void PutItem(Item itemData) {
        _itemData = itemData;
        SetIcon();
    }

    public override void TakeItem() {
        _itemData = null;
        SetIcon();
    }
}
