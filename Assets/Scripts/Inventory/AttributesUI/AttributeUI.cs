using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour {
    protected AttributeType _attributeType;
    protected PlayerConfig _playerConfig;
    protected float _valueInteger;
    protected float _valuePercent;
    protected float _percent;

    [SerializeField]
    protected Text _valueTextComponent;
    [SerializeField]
    protected Image _icon;

    protected delegate void CalculationBaseInteger(Attribute attribute);
    protected delegate ValueType GetValueType();
    protected delegate void CalculationField(Attribute attribute);

    public float Value { get; private set; }

    protected void Awake() {
        EventManager.PutOnItem += CalculationAddPlayerAttribute;
        EventManager.TakeAwayItem += CalculationMinusPlayerAttribute;
    }

    protected void OnDestroy() {
        EventManager.PutOnItem -= CalculationAddPlayerAttribute;
        EventManager.TakeAwayItem -= CalculationMinusPlayerAttribute;
    }

    protected void Start() {
        _playerConfig = Resources.Load<PlayerConfig>(ResourcesPath.PlayerConfig);
    }

    protected virtual void UpdateTextOfAttributes() {
        Value = _valueInteger + _valuePercent;
        _valueTextComponent.text = "" + Value;
    }

    public void CalculationAddPlayerAttribute(Item item) {
        print("call calculation");
        CalculationAttributesValue(item, GetIntegerType, AddInteger);
        CalculationAttributesValue(item, GetPercentType, AddPercent);
        AddPercent(ScriptableObject.CreateInstance<Attribute>());
        UpdateTextOfAttributes();
        EventManager.UpdatingHealthBarEventHandler();
    }

    public void CalculationMinusPlayerAttribute(Item item) {
        CalculationAttributesValue(item, GetIntegerType, MinusInteger);
        CalculationAttributesValue(item, GetPercentType, MinusPercent);
        MinusPercent(ScriptableObject.CreateInstance<Attribute>());
        UpdateTextOfAttributes();
        EventManager.UpdatePlayerCurrentHealthEventHandler();
        EventManager.UpdatingHealthBarEventHandler();
    }

    protected virtual void CalculationAttributesValue(Item item, GetValueType valueType, CalculationField calculationField) {
        foreach (Attribute attribute in item.Attributes) {
            if(attribute.type != _attributeType) {
                continue;
            }

            if(attribute.valueType == valueType.Invoke()) {
                calculationField.Invoke(attribute);
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
        _valuePercent = GetAddPercent(attribute);
    }

    protected float GetAddPercent(Attribute attribute) {
        _percent += attribute.value;
        float _result = _percent * _valueInteger / 100;
        return _result;
    }

    protected virtual void MinusPercent(Attribute attribute) {
        _valuePercent = GetMinusPercent(attribute);
    }

    protected float GetMinusPercent(Attribute attribute) {
        _percent -= attribute.value;
        float _result = _percent * _valueInteger / 100;
        return _result;
    }

    protected ValueType GetIntegerType() {
        return ValueType.Integer;
    }

    protected ValueType GetPercentType() {
        return ValueType.Percent;
    }
}