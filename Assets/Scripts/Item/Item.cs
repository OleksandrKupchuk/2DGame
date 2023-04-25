using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Helmet,
    Armor,
    Ring,
    Amulet,
    Weapon,
    Belt,
    Boots
}

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour {
    [SerializeField]
    private Sprite _icon;
    [field: SerializeField]
    public ItemType ItemType { get; private set; } = new ItemType();
    [field: SerializeField]
    public List<Attribute> Attributes { get; private set; } = new List<Attribute>();
    public Sprite Icon { get => _icon; }
}

public enum ValueType {
    Integer,
    Percent
}

public enum AttributeType {
    Armor,
    Damage,
    Health,
    Speed
}