using UnityEngine;

public class WearableItem : Item {
    [field: SerializeField]
    public ItemType ItemType { get; protected set; } = new ItemType();
    [field: SerializeField]
    public BodyType BodyType { get; protected set; }
}
