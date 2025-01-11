using System.Collections.Generic;
using UnityEngine;

public class PlayerSlot : Cell, ICell {
    private WearableItemData _itemData;

    [SerializeField]
    private List<ItemType> _itemTypes = new List<ItemType>();

    public bool HasItem => _itemData != null;
    public ItemData ItemData { get => _itemData; }
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

    private void ChangeBorderColor(ItemData item) {
        if (item is WearableItemData) {
            WearableItemData _item = item as WearableItemData;

            if (_itemTypes.Contains(_item.ItemType)) {
                SetBorderColor(Color.green);
            }
            else {
                SetBorderColor(Color.red);
            }
        }

    }

    public void SetItem(ItemData itemData) {
        if (itemData is WearableItemData) {
            WearableItemData _item = itemData as WearableItemData;

            if (_itemTypes.Contains(_item.ItemType)) {
                _itemData = _item;
                SetIcon(_itemData.Icon);
                EnableIcon();
                EventManager.OnItemDressedHandler(_itemData);
            }
        }
    }

    public void RemoveItem() {
        EventManager.TakeAwayItemEventHandler(_itemData);
        _itemData = null;
        DisableIcon();
    }

    private void ResetBorderColor() {
        SetBorderColor(Color.white);
    }

    private void SetBorderColor(Color color) {
        _border.color = color;
    }
}
