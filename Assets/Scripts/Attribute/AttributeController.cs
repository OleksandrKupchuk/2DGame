using UnityEngine;

public abstract class AttributeController : MonoBehaviour {
    protected float _valueInteger;
    protected float _valuePercent;
    protected float _percentOfAttribute;
    protected float _valueTemporary;
    protected PlayerConfig _playerConfig;

    [SerializeField]
    protected AttributeView _attributeView;

    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; }  
    public virtual float Value { get => _valueInteger + _valuePercent + _valueTemporary; }
    public virtual string ValueString { get => (_valueInteger + _valuePercent + _valueTemporary).ToString(); }
    public virtual bool IsValueTemplorary { get => _valueTemporary > 0; }

    protected void Awake() {
        EventManager.PutOnItem += AddItemAttributes;
        EventManager.TakeAwayItem += SubstractItemAttributes;
        EventManager.UseItem += AddTemporaryAttribute;
        EventManager.ActionItemOver += SubstractTemporaryAttribute;
        _playerConfig = Resources.Load<PlayerConfig>(ResourcesPath.PlayerConfig);
    }

    protected void OnDestroy() {
        EventManager.PutOnItem -= AddItemAttributes;
        EventManager.TakeAwayItem -= SubstractItemAttributes;
        EventManager.UseItem -= AddTemporaryAttribute;
        EventManager.ActionItemOver -= SubstractTemporaryAttribute;
    }

    public void AddItemAttributes(Item item) {
        AddIntegerAttributes(item);
        AddPercentAttributes(item);

        _attributeView.UpdateAttribute(this);
    }

    public virtual void AddIntegerAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueInteger += attribute.value;
            }
        }
    }

    public virtual void AddPercentAttributes(Item item) {
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

    public virtual void SubstractIntegerAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueInteger -= attribute.value;
            }
        }
    }

    public virtual void SubstractPercentAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Percent) {
                _percentOfAttribute -= attribute.value;
                _valuePercent = _percentOfAttribute * _valueInteger / 100;
            }
        }
    }

    public virtual void AddTemporaryAttribute(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporary += attribute.value;
                _attributeView.UpdateAttribute(this);
                return;
            }
        }
    }

    public virtual void SubstractTemporaryAttribute(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporary -= attribute.value;
                _attributeView.UpdateAttribute(this);
                return;
            }
        }
    }
}
