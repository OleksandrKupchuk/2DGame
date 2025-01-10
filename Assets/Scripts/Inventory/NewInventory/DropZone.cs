using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {
    [SerializeField]
    private InventoryCellView _cellView;

    public void OnDrop(PointerEventData eventData) {
        if(eventData.pointerDrag == null) {
            return;
        }

        if (eventData.pointerDrag.TryGetComponent(out DragAndDrop dragAndDrop)) {
            ItemData _buffer = dragAndDrop.CellView.ItemData;

            dragAndDrop.CellView.PutItem(_cellView.ItemData);

            _cellView.PutItem(_buffer);
        }
    }
}
