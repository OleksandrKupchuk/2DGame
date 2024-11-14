using UnityEngine;

public interface ICell {
    public Item Item { get; }
    public bool HasItem { get; }
    public Transform Transfom { get; }
    public Collider2D Collider { get; }
    public RectTransform RectTransform { get; }
    public void SetItem(Item item);
    public void RemoveItem();
}
