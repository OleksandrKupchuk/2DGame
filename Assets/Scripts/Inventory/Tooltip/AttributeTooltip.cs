using UnityEngine;
using UnityEngine.UI;

public class AttributeTooltip : MonoBehaviour {
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Text _value;
    [SerializeField]
    private RectTransform _rect;

    public RectTransform RectTransform { get => _rect; }

    public void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    public void SetValue(Attribute attribute) {
        if (attribute.type == AttributeType.Damage) {
            if (attribute.valueType == ValueType.Integer) {
                _value.text = "+" + attribute.damageMin + "-" + attribute.damageMax;
            }
            else {
                _value.text = "+" + attribute.value + "%";
            }
        }
        else {
            if (attribute.valueType == ValueType.Integer) {
                _value.text = "+" + attribute.value;
            }
            else {
                _value.text = "+" + attribute.value + "%";
            }
        }
    }
}