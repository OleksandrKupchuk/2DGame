public class AttributeDamageUI : AttributeUI {
    protected float _valueIntegerMin;
    protected float _valueIntegerMax;
    protected float _valuePercentMin;
    protected float _valuePercentMax;

    private new void Start() {
        base.Start();
        _valueIntegerMin = _playerConfig.damageMin;
        _valueIntegerMax = _playerConfig.damageMax;
        _attributeType = AttributeType.Damage;
        _icon.sprite = LoadAttributesIcon.GetIcon(_attributeType);
        UpdateTextOfAttributes();
    }

    protected override void UpdateTextOfAttributes() {
        float resultMin = _valueIntegerMin + _valuePercentMin;
        float resultMax = _valueIntegerMax + _valuePercentMax;
        _valueTextComponent.text = resultMin + "-" + resultMax;
    }

    protected override void AddInteger(Attribute attribute) {
        _valueIntegerMin += attribute.damageMin;
        _valueIntegerMax += attribute.damageMax;
    }

    protected override void MinusInteger(Attribute attribute) {
        _valueIntegerMin -= attribute.damageMin;
        _valueIntegerMax -= attribute.damageMax;
    }

    protected override void AddPercent(Attribute attribute) {
        _percent += attribute.value;
        _valuePercentMin = GetAddPercent(_valueIntegerMin);
        _valuePercentMax = GetAddPercent(_valueIntegerMax);
    }

    protected override void MinusPercent(Attribute attribute) {
        _percent -= attribute.value;
        _valuePercentMin = GetMinusPercent(_valueIntegerMin);
        _valuePercentMax = GetMinusPercent(_valueIntegerMax);
    }

    protected float GetAddPercent(float valueInteger) {
        float _result = _percent * valueInteger / 100;
        return _result;
    }

    protected float GetMinusPercent(float valueInteger) {
        float _result = _percent * valueInteger / 100;
        return _result;
    }
}