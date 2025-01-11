using UnityEngine;
using UnityEngine.UI;

public class InventorySlotView : MonoBehaviour {
    private ItemData _itemData;

    [SerializeField]
    private Image _icon;

    public bool IsEmpty => _itemData == null;
    public ItemData ItemData => _itemData;

    public void PutItem(ItemData itemData) {
        _itemData = itemData;
        SetIcon(_itemData);
    }

    public void TakeItem() {
        _itemData = null;
        SetIcon(_itemData);
    }

    private void SetIcon(ItemData itemData) {
        if (!IsEmpty) {
            _icon.color = new Color(255, 255, 255, 255);
            _icon.sprite = itemData.Icon;
        }
        else {
            _icon.color = new Color(255, 255, 255, 0);
        }
    }
}
