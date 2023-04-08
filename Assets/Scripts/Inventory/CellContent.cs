using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CellContent : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private LayerMask _layerMask;

    private Transform _dragParent;
    private CanvasGroup _canvasGroup;
    public Item RegisteredItem { get; private set; }
    public static event Action<Item> DropItem;
    private RaycastHit2D _raycastHit2D;
    private Vector2 _mousePosition;

    private void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        transform.SetParent(_dragParent);
        _canvasGroup.alpha = 0.5f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        transform.position = new Vector3(_mousePosition.x, _mousePosition.y, 0);
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _raycastHit2D = Physics2D.Raycast(_mousePosition, Vector3.forward, 100, _layerMask);
        if (_raycastHit2D.transform != null) {
            print("name" + _raycastHit2D.transform.name);
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (Mouse.current.rightButton.wasPressedThisFrame) {
            print("right click");
            DropItem(RegisteredItem);
            Destroy(gameObject);
        }
    }

    public void OnEndDrag(PointerEventData eventData) {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        if (_raycastHit2D.collider == null) {
            print("drop item");
            DropItem(RegisteredItem);
            Destroy(gameObject);
        }
    }

    public void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    public void SetDragParent(Transform parent) {
        _dragParent = parent;
    }

    public void SetRegisteredItem(Item item) {
        RegisteredItem = item;
    }
}