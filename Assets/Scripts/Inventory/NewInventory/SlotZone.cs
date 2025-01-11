using UnityEngine;
using UnityEngine.EventSystems;

public class SlotZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField]
    private InventorySlotView _slotView;

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

    public void OnPointerEnter(PointerEventData eventData) {
        //if (eventData.pointerDrag == null) {
        //    return;
        //}

        //if (eventData.pointerDrag.TryGetComponent(out DragAndDrop dragAndDrop)) {
        //    Debug.Log("OnPointerEnter");
        //    dragAndDrop.isDropZone = true;
        //}
    }

    public void OnPointerExit(PointerEventData eventData) {
        //Debug.Log("OnPointerExit");

        //if (eventData.pointerDrag == null) {
        //    return;
        //}

        //Debug.Log("OnPointerExit NOT NULL");

        //if (eventData.pointerDrag.TryGetComponent(out DragAndDrop dragAndDrop)) {
        //    Debug.Log("OnPointerExit");
        //    dragAndDrop.isDropZone = false;
        //}
    }
}
