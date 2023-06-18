using UnityEngine;

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

    public void AddAditionanArmor(Potion potion) {
        _attributeArmorUI.AddAdditionalValue(potion);
    }

    public void AddAditionanDamage(Potion potion) {
        _attributeDamageUI.AddAdditionalValue(potion);
    }

    public void AddAditionanSpeed(Potion potion) {
        _attributeSpeedUI.AddAdditionalValue(potion);
    }

    public void AddAditionanHealthRegeneration(Potion potion) {
        _attributeHealthRegenerationUI.AddAdditionalValue(potion);
    }
}