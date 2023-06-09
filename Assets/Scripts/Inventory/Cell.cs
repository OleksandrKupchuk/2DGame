using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected Image _border;
    [SerializeField]
    protected BoxCollider2D _boxCollider2D;

    public bool IsAvailableForInteraction { get; private set; } = true;
    public bool HasItem { get => Item != null; }
    public Item Item { get; private set; }
    public RectTransform RectTransform { get; private set; }
    public BoxCollider2D BoxCollider2D { get => _boxCollider2D; }

    protected void Awake() {
        DisableIcon();
        RectTransform = GetComponent<RectTransform>();
    }

    public void SetItem(Item item) {
        Item = item;

        if(Item != null) {
            SetIcon(Item.Icon);
            EnableIcon();
        }
        else {
            DisableIcon();
        }
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

    public void SetGreenBorder() {
        _border.color = Color.green;
    }

    public void ResetColorBorder() {
        _border.color = Color.white;
    }

    public void SetRedBorder() {
        _border.color = Color.red;
    }

    public void EnableBoxCollider() {
        _boxCollider2D.enabled = true;
    }

    public void DisableBoxCollider() {
        _boxCollider2D.enabled = false;
    }

    public void SetAvailableForInteraction(bool isAvailable) {
        IsAvailableForInteraction = isAvailable;
    }

    public void SetRectTransformPosition(Vector3 newPosition) {
        RectTransform.localPosition = newPosition;
    }

    public virtual bool IsCanPut(Item item) {
        return true;
    }
}