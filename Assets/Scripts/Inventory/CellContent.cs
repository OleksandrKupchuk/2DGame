using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CellContent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField]
    private Image _icon;

    private Transform _dragParent;

    public void OnBeginDrag(PointerEventData eventData) {
        transform.SetParent(_dragParent);
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector3(_mousePosition.x, _mousePosition.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData) {
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    public void SetDragParent(Transform parent) {
        _dragParent = parent;
    }
}