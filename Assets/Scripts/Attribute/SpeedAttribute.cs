using UnityEngine;

[CreateAssetMenu(fileName = "SpeedAttribute", menuName = "Attributes/Speed")]
public class SpeedAttribute : Attribute {
    public float Speed => Value;

    private new void OnEnable() {
        base.OnEnable();
        AttributeType = AttributeType.Speed;
        _valueInteger = _playerConfig.Speed;
    }
}
