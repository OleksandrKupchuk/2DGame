public class AttributeHealthUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.health;
        _attributeType = AttributeType.Health;
        UpdateTextOfAttributes();
        EventManager.UpdatingHealthBarEventHandler();
    }
}
