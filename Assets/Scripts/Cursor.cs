using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cursor : MonoBehaviour {
    private Vector2 _mousePosition;
    private RaycastHit2D RaycastHit2D { get; set; }

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private LayerMask _layerMaskUI;
    [SerializeField]
    private Canvas _canvas;

    public delegate void DelegateEvent(Item item);
    public Item Item { get; private set; }
    public ItemTooltip ItemTooltip { get; private set; }
    public bool HasItem { get => Item != null; }

    private void Awake() {
        DisableIcon();
        ItemTooltip = FindObjectOfType<ItemTooltip>();
    }

    public void SetItem(Item item) {
        Item = item;
        SetIcon(Item.Icon);
        EnableIcon();
    }

    public void RemoveItem() {
        Item = null;
        DisableIcon();
    }

    private void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    private void EnableIcon() {
        _icon.enabled = true;
    }

    private void DisableIcon() {
        _icon.enabled = false;
    }

    public void FollowTheMouse() {
        _mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 _localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, Mouse.current.position.ReadValue(), _canvas.worldCamera, out _localPosition);
        transform.position = _canvas.transform.TransformPoint(_localPosition);
    }

    public Cell GetCell() {
        RaycastHit2D = Physics2D.Raycast(_mousePosition, Vector3.forward, 100f, _layerMaskUI, -100);

        if (RaycastHit2D.transform != null) {
            Debug.Log("name transform = " + RaycastHit2D.transform.name);
            return RaycastHit2D.transform.gameObject.GetComponent<Cell>();
        }

        return null;
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Cell cell)) {
            if (!cell.HasItem) {
                return;
            }
            ItemTooltip.ShowTooltip(cell.Item, cell.transform.position, cell.RectTransform.rect.height);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.TryGetComponent(out Cell cell)) {
            ItemTooltip.HideTooltip();
        }
    }
}
