using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour {
    protected AttributeType _attributeType;
    protected PlayerConfig _playerConfig;
    protected float _valueInteger;
    protected float _valuePercent;
    protected float _percent;

    [SerializeField]
    protected Sprite _sprite;
    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected Text _valueTextComponent;

    protected delegate void CalculationBaseInteger(Attribute attribute);
    protected delegate ValueType GetValueType();
    protected delegate void CalculationField(Attribute attribute);

    public float Value { get; protected set; }
    public float AdditionalValue { get; private set; }

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
        _icon.sprite = _sprite;
    }

    protected virtual void UpdateTextOfAttributes() {
        Value = _valueInteger + _valuePercent;

        if (AdditionalValue > 0) {
            _valueTextComponent.text = $"{Value} <color=green> + {AdditionalValue}</color>";
        }
        else {
            _valueTextComponent.text = $"{Value}";
        }
    }

    public void CalculationAddPlayerAttribute(Item item) {
        CalculationAttributesForItem(item, GetIntegerType, AddInteger);
        CalculationAttributesForItem(item, GetPercentType, AddPercent);
        UpdateTextOfAttributes();
        EventManager.UpdateAttributesEventHandler();
    }

    public void CalculationMinusPlayerAttribute(Item item) {
        CalculationAttributesForItem(item, GetIntegerType, MinusInteger);
        CalculationAttributesForItem(item, GetPercentType, MinusPercent);
        UpdateTextOfAttributes();
        EventManager.UpdateAttributesEventHandler();
    }

    protected virtual void CalculationAttributesForItem(Item item, GetValueType valueType, CalculationField calculationField) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type != _attributeType) {
                continue;
            }

            if (attribute.valueType == valueType.Invoke()) {
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

    public void AddAdditionalValue(Attribute attribute) {
        AdditionalValue = attribute.value;
        UpdateTextOfAttributes();
        StartCoroutine(DelayBuff(attribute.duration));
    }

    private IEnumerator DelayBuff(float duration) {
        yield return new WaitForSeconds(duration);
        AdditionalValue = 0;
        UpdateTextOfAttributes();
    }
}
