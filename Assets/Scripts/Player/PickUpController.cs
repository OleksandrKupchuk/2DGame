using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {
    private Inventory _inventory;
    private List<Item> _items = new List<Item>();

    [SerializeField]
    private InventoryController _inventoryController;

    public void Init(Inventory inventory) {
        _inventory = inventory;
    }

    private void Awake() {
        //DropItemState.DropItem += UnregisterPickUpItem;
    }

    private void OnDestroy() {
        //DropItemState.DropItem -= UnregisterPickUpItem;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.TryGetComponent(out Item item)) {
            RegisterPickUpItem(item);
            PickUpItem(item);
        }

        if (collision.gameObject.name.Contains("Quest item king")) {
            print("pickup quest item");
            QuestSystem.Instance.AddQuestItem(collision.gameObject);
        }

        if (collision.transform.TryGetComponent(out ItemView itemView)) {
            if (_inventoryController.TryAddItem(itemView.ItemData)) {
                Destroy(itemView.gameObject);
            }
        }
    }

    private void RegisterPickUpItem(Item item) {
        if (_items.Contains(item)) {
            return;
        }
        _items.Add(item);
        item.Disable();
    }

    private void UnregisterPickUpItem(Item item) {
        _items.Remove(item);
    }

    private void PickUpItem(Item item) {
        _inventory?.AddItem(item);
    }
}
