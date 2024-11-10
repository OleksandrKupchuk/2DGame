public class AttributeArmorController : AttributeController {
    private new void Awake() {
        base.Awake();
        _valueInteger = _playerConfig.armor;
        _attributeView.UpdateAttribute(this);
    }
}
