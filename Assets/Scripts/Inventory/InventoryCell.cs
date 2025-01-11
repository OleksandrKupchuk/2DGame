using UnityEngine;

public class InventoryCell : Cell, ICell {
    private ItemData _itemData;

    public bool HasItem { get => _itemData != null; }
    public ItemData ItemData { get => _itemData; }
    public RectTransform RectTransform { get; private set; }
    public Collider2D Collider { get => _collider; }
    public Transform Transform => transform;

    private void Awake() {
        DisableIcon();
        RectTransform = GetComponent<RectTransform>();
    }

    public void SetItem(ItemData itemData) {
        _itemData = itemData;
        SetIcon(_itemData.Icon);
        EnableIcon();
    }

    public void RemoveItem() {
        _itemData = null;
        DisableIcon();
    }
}
