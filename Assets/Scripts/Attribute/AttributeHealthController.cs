public class AttributeHealthController : AttributeController {
    private new void Awake() {
        base.Awake();
        _valueInteger = _playerConfig.health;
        _attributeView.UpdateAttribute(this);
    }
}
