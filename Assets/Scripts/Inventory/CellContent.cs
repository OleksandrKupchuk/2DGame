using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CellContent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField]
    private Image _icon;

    public void OnBeginDrag(PointerEventData eventData) {
        gameObject.transform.SetParent(null);
    }

    public void OnDrag(PointerEventData eventData) {
    }

    public void OnEndDrag(PointerEventData eventData) {
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }
}