public class AttributeDamageUI : AttributeUI, IAttribureController {
    protected float _valueIntegerMin;
    protected float _valueIntegerMax;
    protected float _valuePercentMin;
    protected float _valuePercentMax;
    public float DamageMin { get => _valueIntegerMin + _valuePercentMin + AdditionalMinValue; }
    public float DamageMax { get => _valueIntegerMax + _valuePercentMax + AdditionalMaxValue; }
    public float AdditionalMaxValue { get; protected set; }
    public float AdditionalMinValue { get; protected set; }

    private new void Awake() {
        base.Awake();
        _valueIntegerMin = _playerConfig.damageMin;
        _valueIntegerMax = _playerConfig.damageMax;
        _attributeView.UpdateAttribute(this);
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
        _percentOfAttribute += attribute.value;
        _valuePercentMin = GetCalculationPercent(_valueIntegerMin);
        _valuePercentMax = GetCalculationPercent(_valueIntegerMax);
    }

    protected override void MinusPercent(Attribute attribute) {
        _percentOfAttribute -= attribute.value;
        _valuePercentMin = GetCalculationPercent(_valueIntegerMin);
        _valuePercentMax = GetCalculationPercent(_valueIntegerMax);
    }

    public override void AddAdditionalValue(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                AdditionalMinValue = attribute.damageMin;
                AdditionalMaxValue = attribute.damageMax;
                _attributeView.UpdateAttribute(this);
                StartCoroutine(DelayBuff(attribute.duration));
                return;
            }
        }
    }

    public void AddTemporaryAttribute(Item item) {
    }
}
