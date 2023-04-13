using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _border;
    [SerializeField]
    private BoxCollider2D _boxCollider2D;

    public bool IsAvailableForInteraction { get; private set; } = false;
    public bool HasItem { get => Item != null; }
    public Item Item { get; private set; }

    private void Awake() {
        DisableIcon();
    }

    public void SetItem(Item item) {
        Item = item;
    }

    public void SetAndEnableIcon(Sprite icon) {
        _icon.sprite = icon;
        EnableIcon();
    }

    public void DisableIcon() {
        _icon.gameObject.SetActive(false);
    }

    public void EnableIcon() {
        _icon.gameObject.SetActive(true);
    }

    public void SetGreenBorder() {
        _border.color = Color.green;
    }

    public void SetWhiteBorder() {
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
}