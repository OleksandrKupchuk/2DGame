public class AttributeSpeedUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.speed;
        _attributeType = AttributeType.Speed;
        _icon.sprite = LoadAttributesIcon.GetIcon(_attributeType);
        UpdateTextOfAttributes();
    }
}