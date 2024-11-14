using UnityEngine;

public class InventoryCell : Cell, ICell {
    private Item _item;

    public bool HasItem { get => Item != null; }
    public Item Item { get => _item; }
    public RectTransform RectTransform { get; private set; }
    public Collider2D Collider { get => _collider; }
    public Transform Transfom => transform;

    private void Awake() {
        DisableIcon();
        RectTransform = GetComponent<RectTransform>();
    }

    public void SetItem(Item item) {
        _item = item;
        SetIcon(Item.Icon);
        EnableIcon();
    }

    public void RemoveItem() {
        _item = null;
        DisableIcon();
    }

    public void SetRectTransformPosition(Vector3 newPosition) {
        RectTransform.localPosition = newPosition;
    }
}
