public class AttributeHealthUI : AttributeUI {
    private new void Awake() {
        base.Awake();
        _valueInteger = _playerConfig.health;
        _attributeView.UpdateAttribute(this);
    }
}
