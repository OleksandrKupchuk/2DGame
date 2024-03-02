using UnityEngine;
using static UnityEditor.Progress;

[RequireComponent (typeof(AttributeHealthUI))]
[RequireComponent (typeof(AttributeSpeedUI))]
[RequireComponent (typeof(AttributeArmorUI))]
[RequireComponent (typeof(AttributeDamageUI))]
[RequireComponent (typeof(AttributeHealthRegenerationUI))]
public class PlayerAttributes : MonoBehaviour {
    private AttributeHealthUI _attributeHealthUI;
    private AttributeSpeedUI _attributeSpeedUI;
    private AttributeArmorUI _attributeArmorUI;
    private AttributeDamageUI _attributeDamageUI;
    private AttributeHealthRegenerationUI _attributeHealthRegenerationUI;

    private void Awake() {
        _attributeHealthUI = GetComponent<AttributeHealthUI>();
        _attributeSpeedUI = GetComponent <AttributeSpeedUI>();
        _attributeArmorUI = GetComponent <AttributeArmorUI>();
        _attributeDamageUI = GetComponent <AttributeDamageUI>();
        _attributeHealthRegenerationUI = GetComponent <AttributeHealthRegenerationUI>();
    }

    public float Health { get => _attributeHealthUI.Value; }
    public float Speed { get => _attributeSpeedUI.Value + _attributeSpeedUI.AdditionalValue; }
    public float Armor { get => _attributeArmorUI.Value + _attributeArmorUI.AdditionalValue; }
    public float Damage { get => Random.Range(_attributeDamageUI.DamageMin + _attributeDamageUI.AdditionalValue, _attributeDamageUI.DamageMax + _attributeDamageUI.AdditionalValue); }
    public float HealthRegeneration { get => _attributeHealthRegenerationUI.Value + _attributeHealthRegenerationUI.AdditionalValue; }

    public void AddAditionanArmor(Item item) {
        _attributeArmorUI.AddAdditionalValue(item);
    }

    public void AddAditionanDamage(Item item) {
        _attributeDamageUI.AddAdditionalValue(item);
    }

    public void AddAditionanSpeed(Item item) {
        _attributeSpeedUI.AddAdditionalValue(item);
    }

    public void AddAditionanHealthRegeneration(Item item) {
        _attributeHealthRegenerationUI.AddAdditionalValue(item);
    }
}
