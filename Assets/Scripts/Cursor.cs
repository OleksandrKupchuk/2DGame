using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {
    private Vector2 _mousePosition;
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private LayerMask _layerMaskUI;
    [SerializeField]
    private LayerMask _layerMaskBackgroundInventory;
    [SerializeField]
    private Canvas _canvas;

    public Item Item { get; private set; }
    public RaycastHit2D RaycastHit2D { get; private set; }

    private void Awake() {
        DisableIcon();
    }

    public void SetAndEnableIcon(Sprite icon) {
        _icon.sprite = icon;
        EnableIcon();
    }

    public void DisableIcon() {
        _icon.enabled = false;
    }

    public void EnableIcon() {
        _icon.enabled = true;
    }

    public void SetItem(Item item) {
        Item = item;
    }

    public void FollowTheMouse() {
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 _localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Mouse.current.position.ReadValue(), _canvas.worldCamera, out _localPosition);
        transform.position = _canvas.transform.TransformPoint(_localPosition);
    }

    public void StartRaycast() {
        RaycastHit2D = Physics2D.Raycast(_mousePosition, Vector3.forward, 100f, _layerMaskUI, -100);
        if(RaycastHit2D.transform != null) {
            Debug.Log("name transform = " + RaycastHit2D.transform.name);
        }
    }
}