using UnityEngine;
using UnityEngine.UI;

public class AttributeTooltip : MonoBehaviour {
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Text _value;

    public void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    public void SetValue(string value) {
        _value.text = value;
    }
}
