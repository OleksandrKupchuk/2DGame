using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Helmet,
    Armor,
    Ring,
    Amulet,
    Weapon,
    Belt,
    Shield
}

[RequireComponent(typeof(Rigidbody2D))]
public class Item : MonoBehaviour {
    [SerializeField]
    protected Sprite _icon;
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
    Speed,
    HealthRegeneration
}