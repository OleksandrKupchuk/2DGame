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
    protected Collider2D _collider;
    protected SpriteRenderer _spriteRenderer;
    protected Rigidbody2D _rigidbody;

    [field: SerializeField]
    public string Name { get; protected set; }
    [field: SerializeField]
    public string Description { get; protected set; }
    public Sprite Icon { get; protected set; }
    [field: SerializeField]
    public List<Attribute> Attributes { get; protected set; } = new List<Attribute>();

    protected void Awake() {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        Icon = _spriteRenderer.sprite;

        CheckDuplicateAttributes();

        if(Attributes == null || Attributes.Count == 0 ) {
            Debug.LogError($"{nameof(Attributes)} is null or empty, {gameObject.name}");
        }
    }

    public string GetAttributeValue(Attribute attribute) {
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

    protected void CheckDuplicateAttributes() {
        for (int i = 0; i < Attributes.Count; i++) {
            int _nextAttribute = i + 1;

            if (_nextAttribute == Attributes.Count) {
                return;
            }
            if (Attributes[i].type == Attributes[_nextAttribute].type) {
                CheckDuplicateValueType(Attributes[i], Attributes[_nextAttribute]);
            }
        }
    }

    protected void CheckDuplicateValueType(Attribute first, Attribute second) {
        if (first.valueType == second.valueType) {
            Debug.LogError($"You cannot have two same ValueType for the item '{gameObject.name}'");
        }
    }

    public void Disable() {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Static;
    }
}
