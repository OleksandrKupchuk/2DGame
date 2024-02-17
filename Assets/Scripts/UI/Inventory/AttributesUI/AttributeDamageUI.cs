public class AttributeDamageUI : AttributeUI {
    protected float _valueIntegerMin;
    protected float _valueIntegerMax;
    protected float _valuePercentMin;
    protected float _valuePercentMax;

    public float DamageMin { get => _valueIntegerMin + _valuePercentMin; }
    public float DamageMax { get => _valueIntegerMax + _valuePercentMax; }

    private new void Start() {
        base.Start();
        _valueIntegerMin = _playerConfig.damageMin;
        _valueIntegerMax = _playerConfig.damageMax;
        _attributeType = AttributeType.Damage;
        UpdateTextOfAttributes();
    }

    protected override void UpdateTextOfAttributes() {
        float resultMin = _valueIntegerMin + _valuePercentMin;
        float resultMax = _valueIntegerMax + _valuePercentMax;
        if (AdditionalValue > 0) {
            _valueTextComponent.text = $"{resultMin}-{resultMax} <color=green> + {AdditionalValue}</color>";
        }
        else {
            _valueTextComponent.text = $"{resultMin}-{resultMax}";
        }
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
        _valuePercentMin = GetCalculationAddPercent(_valueIntegerMin);
        _valuePercentMax = GetCalculationAddPercent(_valueIntegerMax);
    }

    protected override void MinusPercent(Attribute attribute) {
        _percent -= attribute.value;
        _valuePercentMin = GetCalculationMinusPercent(_valueIntegerMin);
        _valuePercentMax = GetCalculationMinusPercent(_valueIntegerMax);
    }
}
