using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemUsageSlot : Cell {
    private InputActionReference _inputAction;
    [SerializeField]
    private Text _labelButtonIcon;

    private new void Awake() {
        base.Awake();
    }

    private void Update() {
        if (!HasItem) { return; }

        if (_inputAction.action.triggered) {
            UseItem(Item);
        }
    }

    private void UseItem(Item item) {
        IUse _useItem = item as IUse;

        if (_useItem == null) {
            Debug.Log($"you can't use this item {Item.name}");
            return;
        }

        _useItem.Use();
    }

    private string GetNameButton(string bindingString) {
        return bindingString.Substring(bindingString.Length - 1, 1);
    }

    public void SetInputAction(InputActionReference inputAction) {
        _inputAction = inputAction;
        _labelButtonIcon.text = "" + GetNameButton(_inputAction.action.GetBindingDisplayString());
    }
}
