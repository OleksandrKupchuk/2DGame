using UnityEngine;

[RequireComponent(typeof(AttributeHealthUI))]
[RequireComponent(typeof(AttributeSpeedUI))]
[RequireComponent(typeof(AttributeArmorUI))]
[RequireComponent(typeof(AttributeDamageUI))]
[RequireComponent(typeof(AttributeHealthRegenerationUI))]
public class PlayerAttributes : MonoBehaviour {
    private AttributeHealthUI _attributeHealthUI;
    private AttributeSpeedUI _attributeSpeedUI;
    private AttributeArmorUI _attributeArmorUI;
    private AttributeDamageUI _attributeDamageUI;
    private AttributeHealthRegenerationUI _attributeHealthRegenerationUI;

    private void Awake() {
        _attributeHealthUI = GetComponent<AttributeHealthUI>();
        _attributeSpeedUI = GetComponent<AttributeSpeedUI>();
        _attributeArmorUI = GetComponent<AttributeArmorUI>();
        _attributeDamageUI = GetComponent<AttributeDamageUI>();
        _attributeHealthRegenerationUI = GetComponent<AttributeHealthRegenerationUI>();

        EventManager.UseItem += AddAditionanAttributes;
    }

    private void OnDestroy() {
        EventManager.UseItem -= AddAditionanAttributes;
    }

    public float Health { get => _attributeHealthUI.Value; }
    public float Speed { get => _attributeSpeedUI.Value + _attributeSpeedUI.AdditionalValue; }
    public float Armor { get => _attributeArmorUI.Value + _attributeArmorUI.AdditionalValue; }
    public float Damage { get => Random.Range(_attributeDamageUI.DamageMin + _attributeDamageUI.AdditionalValue, _attributeDamageUI.DamageMax + _attributeDamageUI.AdditionalValue); }
    public float HealthRegeneration { get => _attributeHealthRegenerationUI.Value + _attributeHealthRegenerationUI.AdditionalValue; }

    public void AddAditionanAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            if (attribute.type == AttributeType.Armor) {
                _attributeArmorUI.AddAdditionalValue(attribute);
            }
            else if (attribute.type == AttributeType.Damage) {
                _attributeDamageUI.AddAdditionalValue(attribute);
            }
            else if (attribute.type == AttributeType.Speed) {
                _attributeSpeedUI.AddAdditionalValue(attribute);
            }
            else if (attribute.type == AttributeType.HealthRegeneration) {
                _attributeHealthRegenerationUI.AddAdditionalValue(attribute);
            }
        }
    }
}
