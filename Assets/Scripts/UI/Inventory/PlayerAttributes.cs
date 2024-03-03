using UnityEngine;

[RequireComponent(typeof(AttributeHealthUI))]
[RequireComponent(typeof(AttributeSpeedUI))]
[RequireComponent(typeof(AttributeArmorUI))]
[RequireComponent(typeof(AttributeDamageUI))]
[RequireComponent(typeof(AttributeHealthRegenerationUI))]
public class PlayerAttributes : MonoBehaviour {
    private AttributeUI[] _attributes;
    private AttributeHealthUI _attributeHealthUI;
    private AttributeSpeedUI _attributeSpeedUI;
    private AttributeArmorUI _attributeArmorUI;
    private AttributeDamageUI _attributeDamageUI;
    private AttributeHealthRegenerationUI _attributeHealthRegenerationUI;
    public float Health { get => _attributeHealthUI.Value + _attributeHealthUI.AdditionalValue; }
    public float Speed { get => _attributeSpeedUI.Value + _attributeSpeedUI.AdditionalValue; }
    public float Armor { get => _attributeArmorUI.Value + _attributeArmorUI.AdditionalValue; }
    public float Damage { get => Random.Range(_attributeDamageUI.DamageMin + _attributeDamageUI.AdditionalValue, _attributeDamageUI.DamageMax + _attributeDamageUI.AdditionalValue); }
    public float HealthRegeneration { get => _attributeHealthRegenerationUI.Value + _attributeHealthRegenerationUI.AdditionalValue; }

    private void Awake() {
        _attributes = GetComponents<AttributeUI>();
        _attributeHealthUI = (AttributeHealthUI)GetAttributeType(AttributeType.Health);
        _attributeSpeedUI = (AttributeSpeedUI)GetAttributeType(AttributeType.Speed);
        _attributeArmorUI = (AttributeArmorUI)GetAttributeType(AttributeType.Armor);
        _attributeDamageUI = (AttributeDamageUI)GetAttributeType(AttributeType.Damage);
        _attributeHealthRegenerationUI = (AttributeHealthRegenerationUI)GetAttributeType(AttributeType.HealthRegeneration);

        EventManager.UseItem += AddAditionanAttributes;
    }

    private void OnDestroy() {
        EventManager.UseItem -= AddAditionanAttributes;
    }

    private AttributeUI GetAttributeType(AttributeType attributeType) {
        foreach (var attribute in _attributes) {
            if (attribute.AttributeType == attributeType) {
                return attribute;
            }
        }

        Debug.Log("can't find attribute");
        return null;
    }

    public void AddAditionanAttributes(Item item) {
        foreach (Attribute attribute in item.Attributes) {
            foreach (AttributeUI attributeUI in _attributes) {
                if (attribute.type == attributeUI.AttributeType) {
                    attributeUI.AddAdditionalValue(attribute);
                }
            }
        }
    }
}
