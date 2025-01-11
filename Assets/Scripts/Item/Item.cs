using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Helmet,
    Armor,
    Ring,
    Amulet,
    Weapon,
    Belt,
    Shield
}

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Item : MonoBehaviour {
    [SerializeField]
    protected Collider2D _collider;
    [SerializeField]
    protected Rigidbody2D _rigidbody;
    [SerializeField]
    protected SpriteRenderer _spriteRenderer;
    [field: SerializeField]
    public string Name { get; protected set; }
    [field: SerializeField]
    public string Description { get; protected set; }
    [field: SerializeField]
    public int Price { get; protected set; }
    public Sprite Icon { get => _spriteRenderer.sprite; }
    [field: SerializeField]
    public List<AttributeData> Attributes { get; protected set; } = new List<AttributeData>();

    protected void Awake() {
        if(Attributes == null || Attributes.Count == 0 ) {
            Debug.LogError($"{nameof(Attributes)} is null or empty, {gameObject.name}");
        }
    }

    public string GetAttributeValue(AttributeData attribute) {
        string _value = "";

        if (attribute.type == AttributeType.Damage && attribute.valueType == ValueType.Integer) {
            _value = "+" + attribute.damageMin + "-" + attribute.damageMax;
        }
        else {
            if (attribute.valueType == ValueType.Integer) {
                _value = "+" + attribute.value;
            }
            else {
                _value = "+" + attribute.value + "%";
            }
        }

        return _value;
    }

    public void Disable() {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }
}
