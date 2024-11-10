using System.Collections;
using UnityEngine;

public class AttributeUI : MonoBehaviour {
    protected PlayerConfig _playerConfig;
    protected float _valueInteger;
    protected float _valuePercent;
    protected float _percentOfAttribute;

    [SerializeField]
    protected AttributeView _attributeView;

    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; }  
    public virtual float Value { get => _valueInteger + _valuePercent + AdditionalValue; }
    public float AdditionalValue { get; private set; }

    protected void Awake() {
        EventManager.PutOnItem += AddItemAttributes;
        EventManager.TakeAwayItem += SubstractItemAttributes;
        EventManager.UseItem += AddAdditionalValue;
        _playerConfig = Resources.Load<PlayerConfig>(ResourcesPath.PlayerConfig);
    }

    protected void OnDestroy() {
        EventManager.PutOnItem -= AddItemAttributes;
        EventManager.TakeAwayItem -= SubstractItemAttributes;
        EventManager.UseItem -= AddAdditionalValue;
    }

    public void AddItemAttributes(Item item) {
        AddIntegerAttributes(item);
        AddPercentAttributes(item);

        _attributeView.UpdateAttribute(this);
    }

    public void AddIntegerAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueInteger += attribute.value;
            }
        }
    }

    public void AddPercentAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute += attribute.value;
                _valuePercent = _percentOfAttribute * _valueInteger / 100;
            }
        }
    }

    public void SubstractItemAttributes(Item item) {
        SubstractIntegerAttributes(item);
        SubstractPercentAttributes(item);

        _attributeView.UpdateAttribute(this);
    }

    public void SubstractIntegerAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueInteger -= attribute.value;
            }
        }
    }

    public void SubstractPercentAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute -= attribute.value;
                _valuePercent = _percentOfAttribute * _valueInteger / 100;
            }
        }
    }

    protected virtual void AddInteger(Attribute attribute) {
        _valueInteger += attribute.value;
    }

    protected virtual void MinusInteger(Attribute attribute) {
        _valueInteger -= attribute.value;
    }

    protected virtual void AddPercent(Attribute attribute) {
        _percentOfAttribute += attribute.value;
        _valuePercent = GetCalculationPercent(_valueInteger);
    }

    protected virtual void MinusPercent(Attribute attribute) {
        _percentOfAttribute -= attribute.value;
        _valuePercent = GetCalculationPercent(_valueInteger);
    }

    protected float GetCalculationPercent(float valueInteger) {
        float _result = _percentOfAttribute * valueInteger / 100;
        return _result;
    }

    public virtual void AddAdditionalValue(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                AdditionalValue = attribute.value;
                _attributeView.UpdateAttribute(this);
                StartCoroutine(DelayBuff(attribute.duration));
                return;
            }
        }
    }

    protected IEnumerator DelayBuff(float duration) {
        yield return new WaitForSeconds(duration);
        AdditionalValue = 0;
        _attributeView.UpdateAttribute(this);
        EventManager.ActionItemOverEventHandler(null);
    }
}
