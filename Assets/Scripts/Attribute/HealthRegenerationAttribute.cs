using UnityEngine;

[CreateAssetMenu(fileName = "HealthRegenerationAttribute", menuName = "Attributes/HealthRegeneration")]
public class HealthRegenerationAttribute : Attribute {
    public float HealthRegeneration => Value;

    private new void OnEnable() {
        base.OnEnable();
        _valueInteger = _playerConfig.HealthRegeneration;
        AttributeType = AttributeType.HealthRegeneration;
    }
}
