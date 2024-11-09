using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour {
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

    protected delegate ValueType GetValueType();
    protected delegate void CalculationField(Attribute attribute);

    [field: SerializeField]
    public AttributeType AttributeType { get; protected set; }  
    public float Value { get; protected set; }
    public float AdditionalValue { get; private set; }

    protected void Awake() {
        EventManager.PutOnItem += AddPlayerAttribute;
        EventManager.TakeAwayItem += MinusPlayerAttribute;
        EventManager.UseItem += AddAdditionalValue;
    }

    protected void OnDestroy() {
        EventManager.PutOnItem -= AddPlayerAttribute;
        EventManager.TakeAwayItem -= MinusPlayerAttribute;
        EventManager.UseItem -= AddAdditionalValue;
    }

    protected void Start() {
        _playerConfig = Resources.Load<PlayerConfig>(ResourcesPath.PlayerConfig);
        _icon.sprite = _sprite;
    }

    protected virtual void UpdateTextAttributes() {
        Value = _valueInteger + _valuePercent;

        if (AdditionalValue > 0) {
            _valueTextComponent.text = $"<color=green> {Value + AdditionalValue}</color>";
        }
        else {
            _valueTextComponent.text = $"{Value}";
        }
    }

    public void AddPlayerAttribute(Item item) {
        CalculationAttributesForItem(item, GetIntegerType, AddInteger);
        CalculationAttributesForItem(item, GetPercentType, AddPercent);
        UpdateTextAttributes();
        EventManager.UpdateAttributesEventHandler();
    }

    public void MinusPlayerAttribute(Item item) {
        CalculationAttributesForItem(item, GetIntegerType, MinusInteger);
        CalculationAttributesForItem(item, GetPercentType, MinusPercent);
        UpdateTextAttributes();
        EventManager.UpdateAttributesEventHandler();
    }

    protected void CalculationAttributesForItem(Item item, GetValueType valueType, CalculationField calculationField) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type != AttributeType) {
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

    protected virtual void MinusPercent(Attribute attribute) {
        _percent -= attribute.value;
        _valuePercent = GetCalculationMinusPercent(_valueInteger);
    }

    protected float GetCalculationAddPercent(float valueInteger) {
        float _result = _percent * valueInteger / 100;
        return _result;
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

    public virtual void AddAdditionalValue(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType) {
                AdditionalValue = attribute.value;
                UpdateTextAttributes();
                StartCoroutine(DelayBuff(attribute.duration));
                return;
            }
        }
    }

    protected IEnumerator DelayBuff(float duration) {
        yield return new WaitForSeconds(duration);
        AdditionalValue = 0;
        UpdateTextAttributes();
    }
}
