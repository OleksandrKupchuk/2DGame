using UnityEngine;
using UnityEngine.EventSystems;

public class MarketSlotView : SlotView, IPointerClickHandler {
    [SerializeField]
    private Market _market;
    [SerializeField]
    private PlayerInputReader _playerInput;

    public override void PutItem(ItemData itemData) {
        _itemData = itemData;
        SetIcon();
    }

    public override void TakeItem() {
        _itemData = null;
        SetIcon();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            if (IsEmpty) {
                return;
            }

            Debug.Log("BuyItem item");
            _market.BuyItem(_itemData);
            _market.RemoveItem(_itemData);
        }
    }
}
