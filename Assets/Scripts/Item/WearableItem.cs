using UnityEngine;

[CreateAssetMenu(fileName = "WearableItem", menuName = "Item/WearableItem")]
public class WearableItem : Item {
    [field: SerializeField]
    public ItemType ItemType { get; protected set; }
    [field: SerializeField]
    public BodyType BodyType { get; protected set; }
}
