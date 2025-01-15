using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    private RectTransform _rectTransform;
    private Transform _parent;

    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Inventory _inventoryController;

    [field: SerializeField]
    public SlotView SlotView { get; private set; }

    public bool isDropZone = false;

    public static event Action<ItemData> OnItemTaken;
    public static event Action OnItemPutted;

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _parent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (SlotView.IsEmpty) { return; }
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
        OnItemTaken?.Invoke(SlotView.ItemData);
    }

    public void OnDrag(PointerEventData eventData) {
        if (SlotView.IsEmpty) { return; }
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (isDropZone) {
            _inventoryController.RemoveItem(SlotView.ItemData);
            ResetPowition();
        }
        else {
            ResetPowition();
        }

    }

    private void ResetPowition() {
        transform.SetParent(_parent);
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        OnItemPutted?.Invoke();
    }
}
