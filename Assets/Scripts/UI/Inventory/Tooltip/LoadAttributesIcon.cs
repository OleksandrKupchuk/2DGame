using UnityEngine;

public class LoadAttributesIcon : MonoBehaviour {
    private static Sprite _health;
    private static Sprite _healthRegeneration;
    private static Sprite _armor;
    private static Sprite _speed;
    private static Sprite _damage;

    private void Awake() {
        LoadIcons();
    }

    private void LoadIcons() {
        _health = Resources.Load<Sprite>(ResourcesPath.SpriteHealth);
        _healthRegeneration = Resources.Load<Sprite>(ResourcesPath.SpriteHealthRegeneration);
        _armor = Resources.Load<Sprite>(ResourcesPath.SpriteArmor);
        _speed = Resources.Load<Sprite>(ResourcesPath.SpriteSpeed);
        _damage = Resources.Load<Sprite>(ResourcesPath.SpriteDamage);
    }

    public static Sprite GetIcon(AttributeType attributeType) {
        switch (attributeType) {
            case AttributeType.Armor:
                return _armor;

            case AttributeType.Damage:
                return _damage;

            case AttributeType.Health:
                return _health;

            case AttributeType.HealthRegeneration:
                return _healthRegeneration;

            case AttributeType.Speed:
                return _speed;

            default:
                Debug.LogError($"{attributeType} not exist");
                return null;
        }
    }
}
