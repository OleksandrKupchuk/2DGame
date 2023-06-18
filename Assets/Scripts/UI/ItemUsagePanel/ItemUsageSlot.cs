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
        DragDropController.RaisedItemTrigger += ChageColorBorderCell;
        DragDropController.DropPutItemTrigger += ResetColorBorder;
    }

    private void OnDestroy() {
        DragDropController.RaisedItemTrigger -= ChageColorBorderCell;
        DragDropController.DropPutItemTrigger -= ResetColorBorder;
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
        SetItem(null);
    }

    private string GetNameButton(string bindingString) {
        return bindingString.Substring(bindingString.Length - 1, 1);
    }

    private void ChageColorBorderCell(Item item) {
        Potion _potion = item as Potion;

        if (_potion == null) {
            //print("Item not Potion");
            return;
        }

        SetGreenBorder();
    }

    public override bool IsCanPut(Item item) {
        Potion _potion = item as Potion;

        if (_potion == null) {
            return false;
        }

        return true;
    }

    public void SetInputAction(InputActionReference inputAction) {
        _inputAction = inputAction;
        _labelButtonIcon.text = "" + GetNameButton(_inputAction.action.GetBindingDisplayString());
    }
}