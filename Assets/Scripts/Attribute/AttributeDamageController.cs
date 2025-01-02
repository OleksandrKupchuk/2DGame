using UnityEngine;

public class AttributeDamageController : AttributeController {
    private float _valueIntegerMin;
    private float _valueIntegerMax;
    private float _valuePercentMin;
    private float _valuePercentMax;
    private float _valueTemporaryMin;
    private float _valueTemporaryMax;

    public float DamageMin { get => _valueIntegerMin + _valuePercentMin + _valueTemporaryMin; }
    public float DamageMax { get => _valueIntegerMax + _valuePercentMax + _valueTemporaryMax; }
    public override float Value => Random.Range(DamageMin, DamageMax);
    public override string ValueString => $"{DamageMin}-{DamageMax}";
    public override bool IsValueTemporary => _valueTemporaryMin > 0 || _valueTemporaryMax > 0;

    private new void Awake() {
        base.Awake();
        _valueIntegerMin = _playerConfig.damageMin;
        _valueIntegerMax = _playerConfig.damageMax;
        _attributeView.UpdateAttribute(this);
    }

    public override void AddIntegerAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueIntegerMin += attribute.damageMin;
                _valueIntegerMax += attribute.damageMax;
            }
        }
    }

    public override void AddPercentAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute += attribute.value;
                CalculationPercent();
            }
        }
    }

    public override void SubtractIntegerAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueIntegerMin -= attribute.damageMin;
                _valueIntegerMax -= attribute.damageMax;
            }
        }
    }

    public override void SubtractPercentAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
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

    public override void AddTemporaryAttribute(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporaryMin += attribute.damageMin;
                _valueTemporaryMax += attribute.damageMax;
                _attributeView.UpdateAttribute(this);
                return;
            }
        }
    }

    public override void SubtractTemporaryAttribute(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporaryMin -= attribute.damageMin;
                _valueTemporaryMax -= attribute.damageMax;
                _attributeView.UpdateAttribute(this);
                return;
            }
        }
    }
}
