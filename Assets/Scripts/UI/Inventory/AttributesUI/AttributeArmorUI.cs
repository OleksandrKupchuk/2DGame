public class AttributeArmorUI : AttributeUI {
    private new void Awake() {
        base.Awake();
        _valueInteger = _playerConfig.armor;
        _attributeView.UpdateAttribute(this);
    }
}
