using UnityEngine;

public class PlayerSlot : MonoBehaviour {
    private Cell _cell;
    [SerializeField]
    private ItemType _itemType = new ItemType();

    public Cell Cell { get => _cell; }

    private void Awake() {
        _cell = GetComponent<Cell>();
        DragDropController.RaisedItemTrigger += ChageColorBorderCell;
        DragDropController.DropPutItemTrigger += ResetColorBorderCell;
        _cell.SetAvailableForInteraction(true);
    }

    private void OnDestroy() {
        DragDropController.RaisedItemTrigger -= ChageColorBorderCell;
        DragDropController.DropPutItemTrigger -= ResetColorBorderCell;
    }

    private void ChageColorBorderCell(Item item) {
        if (item.ItemType != _itemType) {
            _cell.SetAvailableForInteraction(false);
            _cell.SetRedBorder();
        }
        else {
            _cell.SetGreenBorder();
        }
    }

    private void ResetColorBorderCell() {
        _cell.SetAvailableForInteraction(true);
        _cell.EnableBoxCollider();
        _cell.SetWhiteBorder();
    }
}