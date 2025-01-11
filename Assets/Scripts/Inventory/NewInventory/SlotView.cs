using UnityEngine;
using UnityEngine.UI;

public abstract class SlotView : MonoBehaviour {
    protected ItemData _itemData;

    [SerializeField]
    protected Image _itemIcon;

    public bool IsEmpty => _itemData == null;
    public ItemData ItemData => _itemData;

    public abstract void PutItem(ItemData itemData);

    public abstract void TakeItem();

    protected void SetIcon() {
        if (!IsEmpty) {
            _itemIcon.color = new Color(255, 255, 255, 255);
            _itemIcon.sprite = _itemData.Icon;
        }
        else {
            _itemIcon.color = new Color(255, 255, 255, 0);
        }
    }
}
