using UnityEngine;
using UnityEngine.UI;

public class InventoryCellView : MonoBehaviour {
    private ItemData _itemData;

    [SerializeField]
    private Image _icon;

    public bool IsEmpty => _itemData == null;
    public ItemData ItemData => _itemData;

    public void PutItem(ItemData itemData) {
        _itemData = itemData;

        if (!IsEmpty) {
            SetIcon(itemData.Icon);
        }
        else {
            _icon.color = new Color(255, 255, 255, 0);
        }
    }

    private void SetIcon(Sprite icon) {
        _icon.color = new Color(255, 255, 255, 255);
        _icon.sprite = icon;
    }
}
