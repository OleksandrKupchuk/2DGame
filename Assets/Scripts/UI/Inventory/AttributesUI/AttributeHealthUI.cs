public class AttributeHealthUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.health;
        _attributeType = AttributeType.Health;
        _icon.sprite = LoadAttributesIcon.GetIcon(_attributeType);
        UpdateTextOfAttributes();
        EventManager.UpdatingHealthBarEventHandler();
    }
}
