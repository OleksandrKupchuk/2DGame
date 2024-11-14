using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemUsageSlot : Cell, ICell {
    private InputActionReference _inputAction;
    private ItemUsable _item;

    [SerializeField]
    private Text _labelButtonIcon;

    public Item Item { get => _item; }

    public bool HasItem => _item != null;
    public RectTransform RectTransform { get; private set; }
    public Collider2D Collider { get => _collider; }
    public Transform Transfom => transform;

    private void Awake() {
        DisableIcon();
        RectTransform = GetComponent<RectTransform>();
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

    private void ResetBorderColor() {
        SetBorderColor(Color.white);
    }

    private void SetBorderColor(Color color) {
        _border.color = color;
    }

    public void SetItem(Item item) {
        if (CanUseItem(item)) {
            _item = item as ItemUsable;
            SetIcon(Item.Icon);
            EnableIcon();
        }
    }

    private bool CanUseItem(Item item) {
        if (item is ItemUsable) {
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
        _item.Use();
        RemoveItem();
    }

    public void RemoveItem() {
        _item = null;
        DisableIcon();
    }

    public void SetInputAction(InputActionReference inputAction) {
        _inputAction = inputAction;
        _labelButtonIcon.text = "" + GetNameButton(_inputAction.action.GetBindingDisplayString());
    }

    private string GetNameButton(string bindingString) {
        return bindingString.Substring(bindingString.Length - 1, 1);
    }
}
