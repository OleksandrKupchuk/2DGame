public class AttributeHealthUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.health;
        UpdateTextAttributes();
        EventManager.UpdateAttributesEventHandler();
    }
}
