using UnityEngine;

[CreateAssetMenu(fileName = "DamageAttribute", menuName = "Attributes/Damage")]
public class DamageAttribute : Attribute {
    private float _valueIntegerMin;
    private float _valueIntegerMax;
    private float _valuePercentMin;
    private float _valuePercentMax;
    private float _valueTemporaryMin;
    private float _valueTemporaryMax;

    public float Damage => Random.Range(DamageMin, DamageMax);

    public float DamageMin { get => _valueIntegerMin + _valuePercentMin + _valueTemporaryMin; }
    public float DamageMax { get => _valueIntegerMax + _valuePercentMax + _valueTemporaryMax; }
    public override string ValueString => $"{DamageMin}-{DamageMax}";
    public override bool IsValueTemporary => _valueTemporaryMin > 0 || _valueTemporaryMax > 0;

    private new void OnEnable() {
        base.OnEnable();
        AttributeType = AttributeType.Damage;
        _valueIntegerMin = _playerConfig.DamageMin;
        _valueIntegerMax = _playerConfig.DamageMax;
    }

    protected override void AddIntegerAttributes(Item item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueIntegerMin += attribute.damageMin;
                _valueIntegerMax += attribute.damageMax;
            }
        }
    }

    protected override void AddPercentAttributes(Item item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute += attribute.value;
                CalculationPercent();
            }
        }
    }

    protected override void SubtractIntegerAttributes(Item item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueIntegerMin -= attribute.damageMin;
                _valueIntegerMax -= attribute.damageMax;
            }
        }
    }

    protected override void SubtractPercentAttributes(Item item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute -= attribute.value;
                CalculationPercent();
            }
        }
    }

    private void CalculationPercent() {
        _valuePercentMin = _percentOfAttribute * _valueIntegerMin / 100;
        _valuePercentMax = _percentOfAttribute * _valueIntegerMax / 100;
    }

    protected override void AddTemporaryAttribute(Item item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporaryMin += attribute.damageMin;
                _valueTemporaryMax += attribute.damageMax;
                CheckAttributeChange(item);
                return;
            }
        }
    }

    protected override void SubtractTemporaryAttribute(Item item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporaryMin -= attribute.damageMin;
                _valueTemporaryMax -= attribute.damageMax;
                CheckAttributeChange(item);
                return;
            }
        }
    }
}
