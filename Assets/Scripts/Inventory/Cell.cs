using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    [SerializeField]
    private Image _icon;

    public bool IsEmptyCell { get => Item == null; }
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
}