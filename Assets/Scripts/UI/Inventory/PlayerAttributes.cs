using UnityEngine;

[RequireComponent(typeof(AttributeHealthController))]
[RequireComponent(typeof(AttributeSpeedController))]
[RequireComponent(typeof(AttributeArmorController))]
[RequireComponent(typeof(AttributeDamageController))]
[RequireComponent(typeof(AttributeHealthRegenerationController))]
public class PlayerAttributes : MonoBehaviour {
    private AttributeHealthController _attributeHealthController;
    private AttributeSpeedController _attributeSpeedController;
    private AttributeArmorController _attributeArmorController;
    private AttributeDamageController _attributeDamageController;
    private AttributeHealthRegenerationController _attributeHealthRegenerationController;
    public float Health { get => _attributeHealthController.Value; }
    public float Speed { get => _attributeSpeedController.Value; }
    public float Armor { get => _attributeArmorController.Value; }
    public float Damage { get => _attributeDamageController.Value; }
    public float HealthRegeneration { get => _attributeHealthRegenerationController.Value; }

    private void Awake() {
        _attributeHealthController = GetComponent<AttributeHealthController>();
        _attributeSpeedController = GetComponent<AttributeSpeedController>();
        _attributeArmorController = GetComponent<AttributeArmorController>();
        _attributeDamageController = GetComponent<AttributeDamageController>();
        _attributeHealthRegenerationController = GetComponent<AttributeHealthRegenerationController>();
    }
}
