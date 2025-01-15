using UnityEngine;
using UnityEngine.EventSystems;

public class SlotZone : MonoBehaviour, IDropHandler {
    [SerializeField]
    private SlotView _slotView;

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag == null) {
            return;
        }

        if (eventData.pointerDrag.TryGetComponent(out DragAndDrop dragAndDrop)) {
            ItemData _buffer = dragAndDrop.SlotView.ItemData;

            dragAndDrop.SlotView.PutItem(_slotView.ItemData);

            _slotView.PutItem(_buffer);
        }
    }
}
