using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributes : MonoBehaviour {
    private float _baseHealth;
    private float _baseArmor;
    private float _baseSpeed;
    private float _baseDamageMin;
    private float _baseDamageMax;
    private float _healthPercent;
    private float _armorPercent;
    private float _speedPercent;
    private float _damageMinPercent;
    private float _damageMaxPercent;
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

    private void OnEnable() {
        EventManager.PutOrTakeAwakeItem += CalculationAttributesOfPlayer;
    }

    private void OnDestroy() {
        EventManager.PutOrTakeAwakeItem -= CalculationAttributesOfPlayer;
    }

    private void Awake() {
        if(_playerSlots.Count == 0) {
            Debug.LogError("Player slots is 0");
        }
        SetValueAttributesTakenFromPlayerConfig();
        UpdateTextOfAttributes();
    }

    private void SetValueAttributesTakenFromPlayerConfig() {
        _baseHealth = _playerConfig.health;
        _baseArmor = _playerConfig.armor;
        _baseSpeed = _playerConfig.speed;
        _baseDamageMin = _playerConfig.damageMin;
        _baseDamageMax = _playerConfig.damageMax;
    }

    private void UpdateTextOfAttributes() {
        float _resultArmor = _baseArmor + _armorPercent;
        _armorValueTextComponent.text = "" + (int)Mathf.Floor(_resultArmor);

        float _resultHealth = _baseHealth + _healthPercent;
        _healthValueTextComponent.text = "" + (int)Mathf.Floor(_resultHealth);

        float _resultSpeed = _baseSpeed + _speedPercent;
        _speedValueTextComponent.text = "" + (int)Mathf.Floor(_resultSpeed);

        int _resultDamageMin = (int)(_baseDamageMin + _damageMinPercent);
        int _resultDamageMax = (int)(_baseDamageMax + _damageMaxPercent);
        _damageValueTextComponent.text = _resultDamageMin + "-" + _resultDamageMax;
        //print("damage min = " + _resultDamageMin);
        //print("damage max = " + _resultDamageMax);
    }

    private void CalculationAttributesOfPlayer() {
        SetValueAttributesTakenFromPlayerConfig();
        ResetPercentValues();

        for (int i = 0; i < _playerSlots.Count; i++) {
            if(_playerSlots[i].Cell.Item == null) {
                continue;
            }
            CalculationIntegerAttributesForItem(_playerSlots[i].Cell.Item);
        }

        for (int i = 0; i < _playerSlots.Count; i++) {
            if (_playerSlots[i].Cell.Item == null) {
                continue;
            }
            CalculationPercentAttributesForItem(_playerSlots[i].Cell.Item);
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

    private void CalculationIntegerAttributesForItem(Item item) {
        for (int i = 0; i < item.Attributes.Count; i++) {
            if(item.Attributes[i].valueType == ValueType.Integer) {
                CalculationBaseIntegerAttributes(item.Attributes[i]);
            }
        }
    }

    private void CalculationPercentAttributesForItem(Item item) {
        for (int i = 0; i < item.Attributes.Count; i++) {
            if (item.Attributes[i].valueType == ValueType.Percent) {
                CalculationPercentAttributes(item.Attributes[i]);
            }
        }
    }

    private void CalculationBaseIntegerAttributes(Attribute attribute) {
        switch (attribute.type) {
            case AttributeType.Armor:
                _baseArmor += attribute.value;
                break;

            case AttributeType.Damage:
                _baseDamageMin += attribute.damageMin;
                _baseDamageMax += attribute.damageMax;
                break;

            case AttributeType.Health:
                _baseHealth += attribute.value;
                break;

            case AttributeType.Speed:
                _baseSpeed += attribute.value;
                break;
        }
    }

    private void CalculationPercentAttributes(Attribute attribute) {
        switch (attribute.type) {
            case AttributeType.Armor:
                float resultArmorPercent = attribute.value * _baseArmor / 100;
                _armorPercent += resultArmorPercent;
                break;

            case AttributeType.Damage:
                float resultDamageMinPercent = attribute.value * _baseDamageMin / 100;
                _damageMinPercent += resultDamageMinPercent;
                float resultDamageMaxPercent = attribute.value * _baseDamageMax / 100;
                _damageMaxPercent += resultDamageMaxPercent;
                break;

            case AttributeType.Health:
                float resultHealthPercent = attribute.value * _baseHealth / 100;
                _healthPercent += resultHealthPercent;
                break;

            case AttributeType.Speed:
                float resultSpeedPercent = attribute.value * _baseSpeed / 100;
                _speedPercent += resultSpeedPercent;
                break;
        }
    }
}