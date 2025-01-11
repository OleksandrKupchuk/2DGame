public class InventorySlotView : SlotView {
    public override void PutItem(ItemData itemData) {
        _itemData = itemData;
        SetIcon(_itemData);
    }

    public override void TakeItem() {
        _itemData = null;
        SetIcon(_itemData);
    }
}
