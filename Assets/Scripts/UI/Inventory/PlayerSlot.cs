using System.Collections.Generic;
using UnityEngine;

public class PlayerSlot : Cell {
    [SerializeField]
    private List<ItemType> _itemTypes = new List<ItemType>();


    private new void Awake() {
        base.Awake();
        DragDropController.RaisedItemTrigger += ChageBorderColor;
        DragDropController.DropPutItemTrigger += ResetBorderColor;
    }

    private void OnDestroy() {
        DragDropController.RaisedItemTrigger -= ChageBorderColor;
        DragDropController.DropPutItemTrigger -= ResetBorderColor;
    }

    private void ChageBorderColor(Item item) {
        if (_itemTypes.Contains(item.ItemType)) {
            SetBorderColor(Color.green);
        }
        else {
            SetBorderColor(Color.red);
        }
    }

    public override void SetItem(Item item) {
        if (_itemTypes.Contains(item.ItemType)) {
            base.SetItem(item);
            EventManager.PutOnItemEventHandler(item);
        }
    }

    public override void RemoveItem() {
        EventManager.TakeAwayItemEventHandler(Item);
        Item = null;
        DisableIcon();
    }
}
