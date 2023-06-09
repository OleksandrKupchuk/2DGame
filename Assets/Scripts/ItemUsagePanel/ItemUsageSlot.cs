using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemUsageSlot : Cell {
    [SerializeField]
    private Text _labelButtonIcon;
    [SerializeField]
    private InputActionReference _useButton;

    private new void Awake() {
        base.Awake();
        _labelButtonIcon.text = "" + GetNameButton(_useButton.action.GetBindingDisplayString());
        DragDropController.RaisedItemTrigger += ChageColorBorderCell;
        DragDropController.DropPutItemTrigger += ResetColorBorder;
    }

    private void OnDestroy() {
        DragDropController.RaisedItemTrigger -= ChageColorBorderCell;
        DragDropController.DropPutItemTrigger -= ResetColorBorder;
    }

    private void Update() {
        UseItem();
    }

    private void UseItem() {
        if (!HasItem) {
            //print("Item usage slot is empty");
            return;
        }

        if (_useButton.action.triggered) {
            Potion _potion = (Potion)Item;
            _potion.Use();
            SetItem(null);
        }
    }

    private string GetNameButton(string bindingString) {
        return bindingString.Substring(bindingString.Length - 1, 1);
    }

    private void ChageColorBorderCell(Item item) {
        Potion _potion = item as Potion;

        if (_potion == null) {
            print("Item not Potion");
            return;
        }

        SetGreenBorder();
    }

    public override bool IsCanPut(Item item) {
        Potion _potion = item as Potion;

        if(_potion == null) {
            return false;
        }

        return true;
    }
}