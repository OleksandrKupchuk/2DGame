using UnityEngine;
using UnityEngine.UI;

public class AttributeTooltip : MonoBehaviour {
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Text _value;
    [SerializeField]

    public void Set(Sprite icon, string value) {
        _icon.sprite = icon;
        _value.text = value;
    }
}
