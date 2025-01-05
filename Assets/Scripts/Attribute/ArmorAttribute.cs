using UnityEngine;

[CreateAssetMenu(fileName = "ArmorAttribute", menuName = "Attributes/Armor")]
public class ArmorAttribute : Attribute {
    public float Armor => Value;

    private new void OnEnable() {
        base.OnEnable();
        _valueInteger = _playerConfig.Armor;
        AttributeType = AttributeType.Armor;
    }
}
