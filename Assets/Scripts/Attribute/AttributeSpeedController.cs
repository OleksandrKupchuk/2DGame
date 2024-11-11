public class AttributeSpeedController : AttributeController {
    private new void Awake() {
        base.Awake();
        _valueInteger = _playerConfig.speed;
        _attributeView.UpdateAttribute(this);
    }
}
