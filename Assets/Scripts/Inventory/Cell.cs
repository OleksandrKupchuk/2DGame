using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    [SerializeField]
    private Image _icon;
    [SerializeField]
    private Image _border;
    [SerializeField]
    private BoxCollider2D _boxCollider2D;

    public bool IsAvailableForInteraction { get; private set; } = true;
    public bool HasItem { get => Item != null; }
    public Item Item { get; private set; }
    public RectTransform RectTransform { get; private set; }
    public BoxCollider2D BoxCollider2D { get => _boxCollider2D; }

    private void Awake() {
        DisableIcon();
        RectTransform = GetComponent<RectTransform>();
    }

    public void PrintPosition(int id) {
        print($"{id} position rect anchored = " + RectTransform.anchoredPosition);
        print($"{id} position rect local = " + RectTransform.localPosition);
        print($"{id} position rect position = " + RectTransform.position);
    }

    public void PutItem(Item item) {
        Item = item;
        if(Item != null) {
            SetIcon(Item.Icon);
            EnableIcon();
        }
    }

    private void SetIcon(Sprite icon) {
        _icon.sprite = icon;
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

    public void SetRectTransformPosition(Vector3 newPosition) {
        RectTransform.localPosition = newPosition;
    }
}