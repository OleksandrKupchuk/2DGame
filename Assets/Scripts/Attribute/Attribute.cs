using UnityEngine;

public abstract class Attribute : ScriptableObject {
    protected float _valueInteger;
    protected float _valuePercent;
    protected float _percentOfAttribute;
    protected float _valueTemporary;

    protected float Value { get => _valueInteger + _valuePercent + _valueTemporary; }

    [SerializeField]
    protected PlayerConfig _playerConfig;

    public AttributeType AttributeType { get; protected set; }
    public virtual string ValueString { get => (_valueInteger + _valuePercent + _valueTemporary).ToString(); }
    public virtual bool IsValueTemporary { get => _valueTemporary > 0; }

    protected void OnEnable() {
        ResetData();
        EventManager.OnItemDressed += AddItemAttributes;
        EventManager.TakeAwayItem += SubtractItemAttributes;
        EventManager.UseItem += AddTemporaryAttribute;
        EventManager.ActionItemOver += SubtractTemporaryAttribute;
    }

    protected void OnDisable() {
        EventManager.OnItemDressed -= AddItemAttributes;
        EventManager.TakeAwayItem -= SubtractItemAttributes;
        EventManager.UseItem -= AddTemporaryAttribute;
        EventManager.ActionItemOver -= SubtractTemporaryAttribute;
    }

    protected virtual void AddItemAttributes(ItemData itemData) {
        AddIntegerAttributes(itemData);
        AddPercentAttributes(itemData);

        CheckAttributeChange(itemData);
    }

    protected virtual void AddIntegerAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueInteger += attribute.value;
            }
        }
    }

    protected virtual void AddPercentAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute += attribute.value;
                _valuePercent = _percentOfAttribute * _valueInteger / 100;
            }
        }
    }

    protected virtual void SubtractItemAttributes(ItemData itemData) {
        SubtractIntegerAttributes(itemData);
        SubtractPercentAttributes(itemData);

        CheckAttributeChange(itemData);
    }

    protected virtual void SubtractIntegerAttributes(ItemData itemData) {
        foreach (AttributeData attribute in itemData.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueInteger -= attribute.value;
            }
        }
    }

    protected virtual void SubtractPercentAttributes(ItemData item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute -= attribute.value;
                _valuePercent = _percentOfAttribute * _valueInteger / 100;
            }
        }
    }

    protected virtual void AddTemporaryAttribute(ItemData item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporary += attribute.value;
                CheckAttributeChange(item);
                return;
            }
        }
    }

    protected virtual void SubtractTemporaryAttribute(ItemData item) {
        foreach (AttributeData attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporary -= attribute.value;
                CheckAttributeChange(item);
                return;
            }
        }
    }

    protected void CheckAttributeChange(ItemData item) {
        foreach (AttributeData attributeData in item.Attributes) {
            if (attributeData.type == AttributeType) {
                EventManager.OnAttributeChangedHandler(AttributeType);
            }
        }
    }

    protected void ResetData() {
        _valueInteger = 0;
        _valuePercent = 0;
        _percentOfAttribute = 0;
        _valueTemporary = 0;
    }
}
