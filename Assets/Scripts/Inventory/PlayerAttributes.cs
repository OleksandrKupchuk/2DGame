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
        EventManager.PutOrTakeAwakeItem += CalculationAttributesForAllPlayerSlots;
    }

    private void OnDestroy() {
        EventManager.PutOrTakeAwakeItem -= CalculationAttributesForAllPlayerSlots;
    }

    private void Awake() {
        if(_playerSlots.Count == 0) {
            Debug.LogError("Player slots is 0");
        }
        SetConfigAttributes();
        UpdateTextAttributes();
    }

    private void SetConfigAttributes() {
        _baseHealth = _playerConfig.health;
        _baseArmor = _playerConfig.armor;
        _baseSpeed = _playerConfig.speed;
        _baseDamageMin = _playerConfig.damageMin;
        _baseDamageMax = _playerConfig.damageMax;
    }

    private void UpdateTextAttributes() {
        float _resultArmor = _baseArmor + _armorPercent;
        _armorValueTextComponent.text = "" + _resultArmor;

        float _resultHealth = _baseHealth + _healthPercent;
        _healthValueTextComponent.text = "" + _resultHealth;

        float _resultSpeed = _baseSpeed + _speedPercent;
        _speedValueTextComponent.text = "" + _resultSpeed;

        _damageValueTextComponent.text = _baseDamageMin + "-" + _baseDamageMax;
    }

    private void CalculationAttributesForAllPlayerSlots() {
        SetConfigAttributes();
        ResetPercentValues();

        for (int i = 0; i < _playerSlots.Count; i++) {
            if(_playerSlots[i].Cell.Item == null) {
                continue;
            }
            CalculationIntegerAttributes(_playerSlots[i].Cell.Item);
        }

        for (int i = 0; i < _playerSlots.Count; i++) {
            if (_playerSlots[i].Cell.Item == null) {
                continue;
            }
            CalculationPercentAttributes(_playerSlots[i].Cell.Item);
        }

        UpdateTextAttributes();
    }

    private void ResetPercentValues() {
        _armorPercent = 0;
        _healthPercent = 0;
        _speedPercent = 0;
    }

    private void CalculationIntegerAttributes(Item item) {
        for (int i = 0; i < item.Attributes.Count; i++) {
            if (item.Attributes[i].type == AttributeType.Damage) {

            }
            else if(item.Attributes[i].valueType == ValueType.Integer) {
                CalculationAttributesInteger(item.Attributes[i]);
            }
        }
    }

    private void CalculationPercentAttributes(Item item) {
        for (int i = 0; i < item.Attributes.Count; i++) {
            if (item.Attributes[i].valueType == ValueType.Percent) {
                CalculationAttributesPercent(item.Attributes[i]);
            }
        }
    }

    private void CalculationAttributesInteger(Attribute attribute) {
        switch (attribute.type) {
            case AttributeType.Armor:
                _baseArmor += attribute.value;
                break;

            case AttributeType.Health:
                _baseHealth += attribute.value;
                break;

            case AttributeType.Speed:
                _baseSpeed += attribute.value;
                break;
        }
    }

    private void CalculationAttributesPercent(Attribute attribute) {
        switch (attribute.type) {
            case AttributeType.Armor:
                float resultArmorPercent = attribute.value * _baseArmor / 100;
                _armorPercent += resultArmorPercent;
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