public class AttributeHealthController : AttributeController {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.health;
        _attributeView.UpdateAttribute(this);
    }
}
