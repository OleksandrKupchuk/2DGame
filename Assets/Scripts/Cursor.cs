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
    private Canvas _canvas;

    public delegate void DelegateEvent();
    public Item Item { get; private set; }
    public RaycastHit2D RaycastHit2D { get; private set; }
    public ItemTooltip ItemTooltip { get; private set; }
    public Cell Cell { get; private set; }

    private void Awake() {
        DisableIcon();
        ItemTooltip = FindObjectOfType<ItemTooltip>();
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

    public void TryGetPlayerSlotComponentAndCallEvent(DelegateEvent delegateEvent) {
        PlayerSlot _playerSlot = RaycastHit2D.transform.GetComponent<PlayerSlot>();
        if (_playerSlot != null) {
            delegateEvent.Invoke();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out Cell cell)) {
            Cell = cell;
            if (!cell.HasItem) {
                return;
            }
            ItemTooltip.ShowAttributes(cell.Item, cell.transform.position, cell.RectTransform.rect.height);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.TryGetComponent(out Cell cell)) {
            ItemTooltip.DisableAttributes();
        }
    }
}