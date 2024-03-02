using UnityEngine;

[CreateAssetMenu(fileName = "Item Attribute", menuName = "Item Attribute/Attribute", order = 1)]
public class Attribute : ScriptableObject {
    public AttributeType type = new AttributeType();
    public ValueType valueType = new ValueType();
    public float value;
    public float damageMin;
    public float damageMax;
    public float duration;
    public Sprite icon;
}

public enum AttributeType {
    Armor,
    Damage,
    Health,
    Speed,
    HealthRegeneration
}

public enum ValueType {
    Integer,
    Percent
}
