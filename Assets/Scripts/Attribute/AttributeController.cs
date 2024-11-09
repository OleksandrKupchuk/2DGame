using System.Collections;
using UnityEngine;

public class AttributeController : MonoBehaviour, IAttribureController {
    protected PlayerConfig _playerConfig;
    protected float _valueInteger;
    protected float _valuePercent;
    protected float _percentOfAttribute;

    [SerializeField]
    protected AttributeView _attributeView;

    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; }
    public float Value { get => _valueInteger + _valuePercent; }
    public float TemporaryValue { get; private set; }

    protected void Awake() {
        EventManager.PutOnItem += AddItemAttributes;
        EventManager.TakeAwayItem += SubstractItemAttributes;
        EventManager.UseItem += AddTemporaryAttribute;
    }

    protected void OnDestroy() {
        EventManager.PutOnItem -= AddItemAttributes;
        EventManager.TakeAwayItem -= SubstractItemAttributes;
        EventManager.UseItem -= AddTemporaryAttribute;
    }

    protected void Start() {
        _playerConfig = Resources.Load<PlayerConfig>(ResourcesPath.PlayerConfig);

        if(AttributeType == AttributeType.Armor) {
            _valueInteger = _playerConfig.armor;
        }
        else if(AttributeType == AttributeType.Health) {
            _valueInteger = _playerConfig.health;
        }
        else if(AttributeType == AttributeType.Speed) { 
            _valueInteger = _playerConfig.speed;
        }
        else if (AttributeType == AttributeType.HealthRegeneration) {
            _valueInteger = _playerConfig.healthRegeneration;
        }
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

    public void AddTemporaryAttribute(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                TemporaryValue = attribute.value;
                _attributeView.UpdateAttribute(this);
                StartCoroutine(DelayTemporaryValue(attribute.duration));
                return;
            }
        }
    }

    protected IEnumerator DelayTemporaryValue(float duration) {
        yield return new WaitForSeconds(duration);
        TemporaryValue = 0;
        _attributeView.UpdateAttribute(this);
    }
}
