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

public class Item : MonoBehaviour {
    [SerializeField]
    private Sprite _icon;
    [field: SerializeField]
    public ItemType ItemType { get; private set; } = new ItemType();
    [field: SerializeField]
    public List<Characteristic> Characteristics { get; private set; } = new List<Characteristic>();
    public Sprite Icon { get => _icon; }
}

[System.Serializable]
public class Characteristic {
    public Indicator indicator = new Indicator();
    public ValueType valueType = new ValueType();
    public float value;
}

public enum ValueType {
    Integer,
    Percent
}

public enum Indicator {
    Armor,
    Damage,
    Health,
    Speed
}