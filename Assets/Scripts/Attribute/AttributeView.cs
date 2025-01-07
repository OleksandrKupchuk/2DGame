using UnityEngine;
using UnityEngine.UI;

public class AttributeView : MonoBehaviour {
    [SerializeField]
    protected Sprite _spriteAttribute;
    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected Text _value;
    [SerializeField]
    private Attribute _attribute;

    private void Awake() {
        EventManager.OnAttributeChanged += UpdateAttributeView;
    }

    private void OnDestroy() {
        EventManager.OnAttributeChanged -= UpdateAttributeView;
    }

    private void Start() {
        _icon.sprite = _spriteAttribute;
        UpdateAttributeView(_attribute.AttributeType);
    }

    public void UpdateAttributeView(AttributeType attributeType) {
        if (_attribute.AttributeType != attributeType) {
            return;
        }
        else if (_attribute.IsValueTemporary) {
            _value.text = $"<color=green>{_attribute.ValueString}</color>";
        }
        else {
            _value.text = $"{_attribute.ValueString}";
        }
    }
}
