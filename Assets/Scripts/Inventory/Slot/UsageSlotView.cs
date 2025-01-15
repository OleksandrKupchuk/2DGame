using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UsageSlotView : SlotView {
    private InputAction _inputAction;
    [SerializeField]
    protected Image _border;
    [SerializeField]
    private Text _label;

    private void Awake() {
        DragAndDrop.OnItemTaken += ChangeBorderColor;
        DragAndDrop.OnItemPutted += ResetBorderColor;
    }

    private void OnDestroy() {
        DragAndDrop.OnItemTaken -= ChangeBorderColor;
        DragAndDrop.OnItemPutted -= ResetBorderColor;
    }

    public override void PutItem(ItemData itemData) {
        if (CanUseItem(itemData)) {
            _itemData = itemData;
            SetIcon();
        }
        else {
            _itemData = null;
            SetIcon();
        }
    }

    public override void TakeItem() {
        _itemData = null;
        SetIcon();
    }

    private bool CanUseItem(ItemData itemData) {
        if (itemData is UsableItemData) {
            return true;
        }

        return false;
    }

    private void ChangeBorderColor(ItemData itemData) {
        if (CanUseItem(itemData)) {
            SetBorderColor(Color.green);
        }
        else {
            SetBorderColor(Color.red);
        }
    }

    private void ResetBorderColor() {
        SetBorderColor(Color.white);
    }

    private void SetBorderColor(Color color) {
        _border.color = color;
    }

    //потрібно переробити
    //private void Update() {
    //    if (!HasItem) { return; }

    //    if (_inputAction.triggered) {
    //        UseItem();
    //    }
    //}

    public void SetInputAction(InputAction inputAction) {
        _inputAction = inputAction;
        InputBinding inputBinding = _inputAction.bindings[0];
        string buttonLabel = InputControlPath.ToHumanReadableString(inputBinding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _label.text = buttonLabel;
    }
}
