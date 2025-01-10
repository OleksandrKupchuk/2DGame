using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    private RectTransform _rectTransform;
    private Transform _parent;

    [SerializeField]
    private CanvasGroup _canvasGroup;

    [field: SerializeField]
    public InventoryCellView CellView { get; private set; }

    private void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        _parent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (CellView.IsEmpty) { return; }
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        if (CellView.IsEmpty) { return; }
        _rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {
        transform.SetParent(_parent);
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
}
