public class AttributeHealthRegenerationUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.healthRegeneration;
        _attributeType = AttributeType.HealthRegeneration;
        UpdateTextOfAttributes();
    }
}
