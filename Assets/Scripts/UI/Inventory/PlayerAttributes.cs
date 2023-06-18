using UnityEngine;

public class PlayerAttributes : MonoBehaviour {
    [SerializeField]
    private AttributeHealthUI _attributeHealthUI;
    [SerializeField]
    private AttributeSpeedUI _attributeSpeedUI;
    [SerializeField]
    private AttributeArmorUI _attributeArmorUI;
    [SerializeField]
    private AttributeDamageUI _attributeDamageUI;
    [SerializeField]
    private AttributeHealthRegenerationUI _attributeHealthRegenerationUI;

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