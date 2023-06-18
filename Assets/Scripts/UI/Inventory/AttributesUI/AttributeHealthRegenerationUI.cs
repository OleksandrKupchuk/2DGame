public class AttributeHealthRegenerationUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.healthRegeneration;
        _attributeType = AttributeType.HealthRegeneration;
        _icon.sprite = LoadAttributesIcon.GetIcon(_attributeType);
        UpdateTextOfAttributes();
    }
}