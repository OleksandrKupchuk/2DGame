using UnityEngine;

[CreateAssetMenu(fileName = "ItemData AttributeData", menuName = "ItemData AttributeData/AttributeData", order = 1)]
public class AttributeData : ScriptableObject {
    public AttributeType type = new AttributeType();
    public ValueType valueType = new ValueType();
    public float value;
    public float damageMin;
    public float damageMax;
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
