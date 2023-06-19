public class AttributeArmorUI : AttributeUI {
    private new void Start() {
        base.Start();
        _valueInteger = _playerConfig.armor;
        _attributeType = AttributeType.Armor;
        _icon.sprite = LoadAttributesIcon.GetIcon(_attributeType);
        UpdateTextOfAttributes();
    }
}