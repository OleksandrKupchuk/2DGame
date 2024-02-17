using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected Image _border;
    [SerializeField]
    protected BoxCollider2D _boxCollider2D;

    public bool HasItem { get => Item != null; }
    public Item Item { get; protected set; }
    public RectTransform RectTransform { get; private set; }
    public BoxCollider2D BoxCollider2D { get => _boxCollider2D; }

    protected void Awake() {
        DisableIcon();
        RectTransform = GetComponent<RectTransform>();
    }

    public virtual void SetItem(Item item) {
        Item = item;
        SetIcon(Item.Icon);
        EnableIcon();
    }

    public virtual void RemoveItem() {
        Item = null;
        DisableIcon();
    }

    protected void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }

    protected void EnableIcon() {
        _icon.gameObject.SetActive(true);
    }

    protected void DisableIcon() {
        _icon.gameObject.SetActive(false);
    }

    public void SetRectTransformPosition(Vector3 newPosition) {
        RectTransform.localPosition = newPosition;
    }
}
