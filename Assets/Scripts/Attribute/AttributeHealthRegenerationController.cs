public class AttributeHealthRegenerationController : AttributeController {
    private new void Awake() {
        base.Awake();
        _valueInteger = _playerConfig.healthRegeneration;
        _attributeView.UpdateAttribute(this);
    }
}
