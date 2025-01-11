using UnityEngine;

public interface ICell {
    public ItemData ItemData { get; }
    public bool HasItem { get; }
    public Transform Transform { get; }
    public Collider2D Collider { get; }
    public RectTransform RectTransform { get; }
    public void SetItem(ItemData itemData);
    public void RemoveItem();
}
