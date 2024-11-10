public class AttributeHealthRegenerationUI : AttributeUI {
    private new void Awake() {
        base.Awake();
        _valueInteger = _playerConfig.healthRegeneration;
        _attributeView.UpdateAttribute(this);
    }
}
