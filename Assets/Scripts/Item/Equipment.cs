using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item {
    [field: SerializeField]
    public ItemType ItemType { get; private set; } = new ItemType();
    [field: SerializeField]
    public List<Attribute> Attributes { get; private set; } = new List<Attribute>();

    public override void ShowTooltip(List<AttributeTooltip> attributeTooltips) {
        for (int i = 0; i < Attributes.Count; i++) {
            Sprite _icon = LoadAttributesIcon.GetIcon(Attributes[i].type);
            attributeTooltips[i].SetValue(GetAttributeString(Attributes[i]));
            attributeTooltips[i].SetIcon(_icon);
            attributeTooltips[i].gameObject.SetActive(true);
        }
    }

    private string GetAttributeString(Attribute attribute) {
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

    private new void Awake() {
        base.Awake();
        CheckDuplicateAttributes();
    }

    private void CheckDuplicateAttributes() {
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

    private void CheckDuplicateValueType(Attribute first, Attribute second) {
        if (first.valueType == second.valueType) {
            Debug.LogError($"You cannot have two same ValueType for the item '{gameObject.name}'");
        }
    }
}