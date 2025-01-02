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
    public virtual bool IsValueTemporary { get => _valueTemporary > 0; }

    protected void Awake() {
        EventManager.OnItemDressed += AddItemAttributes;
        EventManager.TakeAwayItem += SubtractItemAttributes;
        EventManager.UseItem += AddTemporaryAttribute;
        EventManager.ActionItemOver += SubtractTemporaryAttribute;
        _playerConfig = Resources.Load<PlayerConfig>(ResourcesPath.PlayerConfig);
    }

    protected void OnDestroy() {
        EventManager.OnItemDressed -= AddItemAttributes;
        EventManager.TakeAwayItem -= SubtractItemAttributes;
        EventManager.UseItem -= AddTemporaryAttribute;
        EventManager.ActionItemOver -= SubtractTemporaryAttribute;
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

    public void SubtractItemAttributes(Item item) {
        SubtractIntegerAttributes(item);
        SubtractPercentAttributes(item);

        _attributeView.UpdateAttribute(this);
    }

    public virtual void SubtractIntegerAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType && attribute.valueType == ValueType.Integer) {
                _valueInteger -= attribute.value;
            }
        }
    }

    public virtual void SubtractPercentAttributes(Item item) {
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

    public virtual void SubtractTemporaryAttribute(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                _valueTemporary -= attribute.value;
                _attributeView.UpdateAttribute(this);
                return;
            }
        }
    }
}
