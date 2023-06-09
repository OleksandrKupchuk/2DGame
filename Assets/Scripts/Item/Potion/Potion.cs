using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : Item {
    private Text _description;
    private Player _player;

    [field: SerializeField]
    public float Value { get; private set; }

    private new void Awake() {
        base.Awake();
        _description = GetComponent<Text>();
    }

    private void Start() {
        _player = FindAnyObjectByType<Player>();
    }

    public override void ShowTooltip(List<AttributeTooltip> attributeTooltips) {
        attributeTooltips[0].SetValue(_description.text);
        attributeTooltips[0].SetIcon(_icon);
        attributeTooltips[0].gameObject.SetActive(true);
    }

    public override void Use() {
        _player.AddHealth(Value);
    }
}