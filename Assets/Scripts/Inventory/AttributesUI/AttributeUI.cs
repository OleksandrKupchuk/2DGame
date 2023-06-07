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

    public void CalculationAddPlayerAttribute(Equipment equipment ) {
        print("call calculation");
        CalculationAttributesForItem(equipment, GetIntegerType, AddInteger);
        CalculationAttributesForItem(equipment, GetPercentType, AddPercent);
        AddPercent(ScriptableObject.CreateInstance<Attribute>());
        UpdateTextOfAttributes();
        EventManager.UpdatingHealthBarEventHandler();
    }

    public void CalculationMinusPlayerAttribute(Equipment equipment) {
        CalculationAttributesForItem(equipment, GetIntegerType, MinusInteger);
        CalculationAttributesForItem(equipment, GetPercentType, MinusPercent);
        MinusPercent(ScriptableObject.CreateInstance<Attribute>());
        UpdateTextOfAttributes();
        EventManager.UpdatePlayerCurrentHealthEventHandler();
        EventManager.UpdatingHealthBarEventHandler();
    }

    protected virtual void CalculationAttributesForItem(Equipment equipment, GetValueType valueType, CalculationField calculationField) {
        foreach (Attribute attribute in equipment.Attributes) {
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
        _percent += attribute.value;
        _valuePercent = GetCalculationAddPercent(_valueInteger);
    }

    protected float GetCalculationAddPercent(float valueInteger) {
        float _result = _percent * valueInteger / 100;
        return _result;
    }

    protected virtual void MinusPercent(Attribute attribute) {
        _percent -= attribute.value;
        _valuePercent = GetCalculationMinusPercent(_valueInteger);
    }

    protected float GetCalculationMinusPercent(float valueInteger) {
        float _result = _percent * valueInteger / 100;
        return _result;
    }

    protected ValueType GetIntegerType() {
        return ValueType.Integer;
    }

    protected ValueType GetPercentType() {
        return ValueType.Percent;
    }
}