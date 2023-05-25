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

    public float Health { get => _attributeHealthUI.Value; }
    public float Speed { get => _attributeSpeedUI.Value; }
    public float Armor { get => _attributeArmorUI.Value; }
    public float Damage { get => Random.Range(_attributeDamageUI.DamageMin, _attributeDamageUI.DamageMax); }
}