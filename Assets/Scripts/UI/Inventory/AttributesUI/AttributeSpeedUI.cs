public class AttributeSpeedUI : AttributeUI {
    private new void Awake() {
        base.Awake();
        _valueInteger = _playerConfig.speed;
        _attributeView.UpdateAttribute(this);
    }
}
