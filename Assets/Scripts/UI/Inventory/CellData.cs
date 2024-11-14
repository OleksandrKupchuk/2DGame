using UnityEngine;
using UnityEngine.UI;

public class CellData : MonoBehaviour {
    [SerializeField]
    protected Image _icon;
    [SerializeField]
    protected Image _border;
    [SerializeField]
    protected Collider2D _collider;

    protected void EnableIcon() {
        _icon.gameObject.SetActive(true);
    }

    protected void DisableIcon() {
        _icon.gameObject.SetActive(false);
    }

    protected void SetIcon(Sprite icon) {
        _icon.sprite = icon;
    }
}
