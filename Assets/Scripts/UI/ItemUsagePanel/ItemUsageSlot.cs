using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemUsageSlot : Cell {
    private InputActionReference _inputAction;
    [SerializeField]
    private Text _labelButtonIcon;
    [SerializeField]
    private InputActionReference _inputActionTest;

    private new void Awake() {
        base.Awake();
    }

    private void Update() {
        if (_inputAction.action.triggered) {
            UseItem(Item);
        }
    }

    private void UseItem(IUse use) {
        if (!HasItem) {
            return;
        }

        use.Use();
        RemoveItem();
    }

    private string GetNameButton(string bindingString) {
        return bindingString.Substring(bindingString.Length - 1, 1);
    }

    public void SetInputAction(InputActionReference inputAction) {
        _inputAction = inputAction;
        _labelButtonIcon.text = "" + GetNameButton(_inputAction.action.GetBindingDisplayString());
    }
}
