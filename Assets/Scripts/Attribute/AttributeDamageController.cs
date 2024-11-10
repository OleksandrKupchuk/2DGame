using System.Collections;
using UnityEngine;

public class AttributeDamageController : AttributeController {
    private float _valueIntegerMin;
    private float _valueIntegerMax;
    private float _valuePercentMin;
    private float _valuePercentMax;
    private float _temporaryMinValue;
    private float _temporaryMaxValue;

    public float DamageMin { get => _valueIntegerMin + _valuePercentMin + _temporaryMinValue; }
    public float DamageMax { get => _valueIntegerMax + _valuePercentMax + _temporaryMaxValue; }
    public override float Value => Random.Range(DamageMin, DamageMax);
    public override string ValueString => $"{DamageMin}-{DamageMax}";
    public override bool IsValueTemplorary => _temporaryMinValue > 0 || _temporaryMaxValue > 0;

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

    public override void SubstractIntegerAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueIntegerMin -= attribute.damageMin;
                _valueIntegerMax -= attribute.damageMax;
            }
        }
    }

    public override void SubstractPercentAttributes(Item item) {
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
                _temporaryMinValue = attribute.damageMin;
                _temporaryMaxValue = attribute.damageMax;
                _attributeView.UpdateAttribute(this);
                StartCoroutine(DelayBuff(attribute.duration));
                return;
            }
        }
    }

    private IEnumerator DelayBuff(float duration) {
        yield return new WaitForSeconds(duration);
        _temporaryMinValue = 0;
        _temporaryMaxValue = 0;
        _attributeView.UpdateAttribute(this);
        EventManager.ActionItemOverEventHandler(null);
    }
}
