public class AttributeSpeedUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.speed;
        UpdateTextAttributes();
    }
}
