using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ItemUsageSlot : Cell, ICell {
    private InputAction _inputAction;
    private UsableItemData _itemData;

    [SerializeField]
    private Text _label;

    public ItemData ItemData { get => _itemData; }
    public bool HasItem => _itemData != null;
    public RectTransform RectTransform { get; private set; }
    public Collider2D Collider { get => _collider; }
    public Transform Transform => transform;

    private void Awake() {
        DisableIcon();
        RectTransform = GetComponent<RectTransform>();
        DragAndDrop.OnItemTaken += ChangeBorderColor;
        DragAndDrop.OnItemPutted += ResetBorderColor;
    }

    private void OnDestroy() {
        DragAndDrop.OnItemTaken -= ChangeBorderColor;
        DragAndDrop.OnItemPutted -= ResetBorderColor;
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

    public void SetItem(ItemData itemData) {
        if (CanUseItem(itemData)) {
            _itemData = itemData as UsableItemData;
            SetIcon(_itemData.Icon);
            EnableIcon();
        }
    }

    private bool CanUseItem(ItemData itemData) {
        if (itemData is UsableItemData) {
            return true;
        }

        return false;
    }

    private void Update() {
        if (!HasItem) { return; }

        if (_inputAction.triggered) {
            UseItem();
        }
    }

    private void UseItem() {
        _itemData.Use();
        RemoveItem();
    }

    public void RemoveItem() {
        _itemData = null;
        DisableIcon();
    }

    public void SetInputAction(InputAction inputAction) {
        _inputAction = inputAction;
        InputBinding inputBinding = _inputAction.bindings[0];
        string buttonLabel = InputControlPath.ToHumanReadableString(inputBinding.effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        _label.text = buttonLabel;
    }
}
