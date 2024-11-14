using System.Collections.Generic;
using UnityEngine;

public class PlayerSlot : CellData, ICell {
    private WearableItem _item;

    [SerializeField]
    private List<ItemType> _itemTypes = new List<ItemType>();

    public bool HasItem => _item != null;
    public Item Item { get => _item; }
    public RectTransform RectTransform { get; private set; }
    public Collider2D Collider { get => _collider; }
    public Transform Transfom => transform;

    private void Awake() {
        DisableIcon();
        RectTransform = GetComponent<RectTransform>();
        DragDropController.RaisedItemTrigger += ChageBorderColor;
        DragDropController.DropPutItemTrigger += ResetBorderColor;
    }

    private void OnDestroy() {
        DragDropController.RaisedItemTrigger -= ChageBorderColor;
        DragDropController.DropPutItemTrigger -= ResetBorderColor;
    }

    private void ChageBorderColor(Item item) {
        if (item is WearableItem) {
            WearableItem _item = item as WearableItem;

            if (_itemTypes.Contains(_item.ItemType)) {
                SetBorderColor(Color.green);
            }
            else {
                SetBorderColor(Color.red);
            }
        }

    }

    public void SetItem(Item item) {
        if (item is WearableItem) {
            WearableItem _item = item as WearableItem;

            if (_itemTypes.Contains(_item.ItemType)) {
                this._item = _item;
                SetIcon(Item.Icon);
                EnableIcon();
                EventManager.PutOnItemEventHandler(_item);
            }
        }
    }

    public void RemoveItem() {
        EventManager.TakeAwayItemEventHandler(_item);
        _item = null;
        DisableIcon();
    }

    public void ResetBorderColor() {
        SetBorderColor(Color.white);
    }

    public void SetBorderColor(Color color) {
        _border.color = color;
    }
}
