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
public abstract class Item : MonoBehaviour, IUse {
    protected Sprite _icon;
    public Sprite Icon { get => _icon; }

    protected void Awake() {
        _icon = GetComponent<SpriteRenderer>().sprite;
    }

    public abstract void ShowTooltip(List<AttributeTooltip> attributeTooltips);

    public void Use() {}
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