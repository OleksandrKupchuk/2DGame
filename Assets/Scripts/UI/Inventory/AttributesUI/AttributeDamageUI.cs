public class AttributeDamageUI : AttributeUI {
    protected float _valueIntegerMin;
    protected float _valueIntegerMax;
    protected float _valuePercentMin;
    protected float _valuePercentMax;
    public float DamageMin { get => _valueIntegerMin + _valuePercentMin; }
    public float DamageMax { get => _valueIntegerMax + _valuePercentMax; }
    public float AdditionalMaxValue { get; protected set; }
    public float AdditionalMinValue { get; protected set; }

    private new void Start() {
        base.Start();
        _valueIntegerMin = _playerConfig.damageMin;
        _valueIntegerMax = _playerConfig.damageMax;
        UpdateTextAttributes();
    }

    protected override void UpdateTextAttributes() {
        float resultMin = _valueIntegerMin + _valuePercentMin;
        float resultMax = _valueIntegerMax + _valuePercentMax;

        if (AdditionalMinValue > 0) {
            _valueTextComponent.text = $"<color=green>{resultMin + AdditionalMinValue}-{resultMax + AdditionalMaxValue}</color>";
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

    public override void AddAdditionalValue(Attribute attribute) {
        AdditionalMinValue = attribute.damageMin;
        AdditionalMaxValue = attribute.damageMax;
        UpdateTextAttributes();
        StartCoroutine(DelayBuff(attribute.duration));
    }
}
