using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : Item {
    protected Text _description;
    protected Player _player;

    [field: SerializeField]
    public float Value { get; private set; }
    [field: SerializeField]
    public float Duration { get; private set; }

    protected new void Awake() {
        base.Awake();
        _description = GetComponent<Text>();
    }

    protected void Start() {
        _player = FindAnyObjectByType<Player>();
    }

    public override void ShowTooltip(List<AttributeTooltip> attributeTooltips) {
        attributeTooltips[0].SetValue(_description.text);
        attributeTooltips[0].SetIcon(_icon);
        attributeTooltips[0].gameObject.SetActive(true);
    }

    public virtual void Use() { }
}