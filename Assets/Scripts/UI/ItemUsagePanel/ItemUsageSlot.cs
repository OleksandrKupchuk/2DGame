using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemUsageSlot : Cell {
    private InputActionReference _inputAction;
    [SerializeField]
    private Text _labelButtonIcon;

    private new void Awake() {
        base.Awake();
        DragDropController.RaisedItemTrigger += ChangeBorderColor;
        DragDropController.DropPutItemTrigger += ResetBorderColor;
    }

    private void OnDestroy() {
        DragDropController.RaisedItemTrigger -= ChangeBorderColor;
        DragDropController.DropPutItemTrigger -= ResetBorderColor;
    }

    private void ChangeBorderColor(Item item) {
        if (CanUseItem(item)) {
            SetBorderColor(Color.green);
        }
        else {
            SetBorderColor(Color.red);
        }
    }

    public override void SetItem(Item item) {
        if (CanUseItem(item)) {
            Item = item;
            SetIcon(item.Icon);
            EnableIcon();
        }
    }

    private bool CanUseItem(Item item) {
        if (item.ItemType == ItemType.Usage) {
            return true;
        }

        return false;
    }

    private void Update() {
        if (!HasItem) { return; }

        if (_inputAction.action.triggered) {
            UseItem();
        }
    }

    private void UseItem() {
        Item.Use();
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
