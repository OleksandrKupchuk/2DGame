using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {
    private float _health;
    private float _armor;
    private float _speed;
    private float _damageMin;
    private float _damageMax;
    private float _healthPercent;
    private float _armorPercent;
    private float _speedPercent;
    private float _damageMinPercent;
    private float _damageMaxPercent;

    private delegate void CalculationBaseInteger(Attribute attribute);
    private delegate ValueType GetValueType();

    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private Text _healthValueTextComponent;
    [SerializeField]
    private Text _armorValueTextComponent;
    [SerializeField]
    private Text _damageValueTextComponent;
    [SerializeField]
    private Text _speedValueTextComponent;
    [SerializeField]
    private List<PlayerSlot> _playerSlots = new List<PlayerSlot>();

    public float ResultArmor { get; private set; }
    public float ResultHealth { get; private set; }
    public float DifferentHealthBetweenPreviousAndCurrent { get; private set; }

    private void OnEnable() {
        EventManager.PutOnOrTakenAwakeItem += CalculationAttributesOfPlayer;
    }

    private void OnDestroy() {
        EventManager.PutOnOrTakenAwakeItem -= CalculationAttributesOfPlayer;
    }

    private void Start() {
        if(_playerSlots.Count == 0) {
            Debug.LogError("Player slots is 0");
        }
        SetValueAttributesTakenFromPlayerConfig();
        UpdateTextOfAttributes();
    }

    private void SetValueAttributesTakenFromPlayerConfig() {
        _health = _playerConfig.health;
        _armor = _playerConfig.armor;
        _speed = _playerConfig.speed;
        _damageMin = _playerConfig.damageMin;
        _damageMax = _playerConfig.damageMax;
    }

    private void UpdateTextOfAttributes() {
        ResultArmor = _armor + _armorPercent;
        _armorValueTextComponent.text = "" + (int)Mathf.Floor(ResultArmor);

        ResultHealth = _health + _healthPercent;
        _healthValueTextComponent.text = "" + (int)Mathf.Floor(ResultHealth);

        float _resultSpeed = _speed + _speedPercent;
        _speedValueTextComponent.text = "" + (int)Mathf.Floor(_resultSpeed);

        int _resultDamageMin = (int)(_damageMin + _damageMinPercent);
        int _resultDamageMax = (int)(_damageMax + _damageMaxPercent);
        _damageValueTextComponent.text = _resultDamageMin + "-" + _resultDamageMax;

        EventManager.TookOffItemEventHandler();
        EventManager.UpdatingHealthBarEventHandler();
    }

    private void CalculationAttributesOfPlayer() {
        SetValueAttributesTakenFromPlayerConfig();
        ResetPercentValues();

        for (int i = 0; i < _playerSlots.Count; i++) {
            if(_playerSlots[i].Cell.Item == null) {
                continue;
            }
            CalculationAttributesForItem(_playerSlots[i].Cell.Item, GetIntegerType, CalculationBaseIntegerAttributes);
        }

        for (int i = 0; i < _playerSlots.Count; i++) {
            if (_playerSlots[i].Cell.Item == null) {
                continue;
            }
            CalculationAttributesForItem(_playerSlots[i].Cell.Item, GetPercentType, CalculationPercentAttributes);
        }

        UpdateTextOfAttributes();
    }

    private void ResetPercentValues() {
        _armorPercent = 0;
        _healthPercent = 0;
        _speedPercent = 0;
        _damageMinPercent = 0;
        _damageMaxPercent = 0;
    }

    private void CalculationAttributesForItem(Item item, GetValueType valueType, CalculationBaseInteger calculationInteger) {
        for (int i = 0; i < item.Attributes.Count; i++) {
            if (item.Attributes[i].valueType == valueType.Invoke()) {
                calculationInteger(item.Attributes[i]);
            }
        }
    }

    private ValueType GetIntegerType() {
        return ValueType.Integer;
    }

    private ValueType GetPercentType() {
        return ValueType.Percent;
    }

    private void CalculationBaseIntegerAttributes(Attribute attribute) {
        switch (attribute.type) {
            case AttributeType.Armor:
                _armor += attribute.value;
                break;

            case AttributeType.Damage:
                _damageMin += attribute.damageMin;
                _damageMax += attribute.damageMax;
                break;

            case AttributeType.Health:
                _health += attribute.value;
                break;

            case AttributeType.Speed:
                _speed += attribute.value;
                break;
        }
    }

    private void CalculationPercentAttributes(Attribute attribute) {
        switch (attribute.type) {
            case AttributeType.Armor:
                _armorPercent += GetCalculationPercent(attribute, _armor);
                break;

            case AttributeType.Damage:
                _damageMinPercent += GetCalculationPercent(attribute, _damageMin);
                _damageMaxPercent += GetCalculationPercent(attribute, _damageMax);
                break;

            case AttributeType.Health:
                _healthPercent += GetCalculationPercent(attribute, _health);
                break;

            case AttributeType.Speed:
                _speedPercent += GetCalculationPercent(attribute, _speed);
                break;
        }
    }

    private float GetCalculationPercent(Attribute attribute, float integerValue) {
        float _result = attribute.value * integerValue / 100;
        return _result;
    }
}