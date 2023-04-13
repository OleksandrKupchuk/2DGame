using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private LayerMask _layerMask;
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
        Vector2 _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 _localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Mouse.current.position.ReadValue(), _canvas.worldCamera, out _localPosition);
        transform.position = _canvas.transform.TransformPoint(_localPosition);
        RaycastHit2D = Physics2D.Raycast(_mousePosition, Vector3.forward, 100f, _layerMask);
    }
}