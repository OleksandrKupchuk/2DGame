using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item {
    [field: SerializeField]
    public ItemType ItemType { get; private set; } = new ItemType();
    [field: SerializeField]
    public List<Attribute> Attributes { get; private set; } = new List<Attribute>();

    private void Awake() {
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