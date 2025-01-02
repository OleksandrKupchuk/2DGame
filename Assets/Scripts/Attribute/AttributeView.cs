using UnityEngine;
using UnityEngine.UI;

public class AttributeView : MonoBehaviour {
    [SerializeField]
    protected Sprite _spriteAttribute;
    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected Text _value;

    private void Start() {
        _icon.sprite = _spriteAttribute;
    }

    public void UpdateAttribute(AttributeController attributeController) {
        if (attributeController.IsValueTemporary) {
            _value.text = $"<color=green>{attributeController.ValueString}</color>";
        }
        else {
            _value.text = $"{attributeController.ValueString}";
        }
    }
}
