using UnityEngine;

[CreateAssetMenu(fileName = "HealthAttribute", menuName = "Attributes/Health")]
public class HealthAttribute : Attribute {
    public float MaxHealth => Value;

    private new void OnEnable() {
        base.OnEnable();
        _valueInteger = _playerConfig.Health;
        AttributeType = AttributeType.Health;
    }

    protected override void AddItemAttributes(ItemData item) {
        base.AddItemAttributes(item);
        EventManager.OnHealthChangedHandler();
    }

    protected override void SubtractItemAttributes(ItemData item) { 
        base.SubtractItemAttributes(item);
        EventManager.OnHealthChangedHandler();
    }
}
