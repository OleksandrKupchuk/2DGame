using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {
    private Inventory _inventory;
    private QuestSystem _playerQuestSystem;
    private List<Item> _items = new List<Item>();

    public void Init(Inventory inventory, QuestSystem playerQuestSystem) {
        _inventory = inventory;
        _playerQuestSystem = playerQuestSystem;
    }

    private void Awake() {
        DropItemState.DropItem += UnregisterPickUpItem;
    }

    private void OnDestroy() {
        DropItemState.DropItem -= UnregisterPickUpItem;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.TryGetComponent(out Item item)) {
            RegisterPickUpItem(item);
            PickUpItem(item);
        }

        if (collision.gameObject.name.Contains("Quest item king")) {
            print("pickup quest item");
            _playerQuestSystem.AddQuestItem(collision.gameObject);
        }
    }

    private void RegisterPickUpItem(Item item) {
        if (_items.Contains(item)) {
            return;
        }
        _items.Add(item);
    }

    private void UnregisterPickUpItem(Item item) {
        _items.Remove(item);
    }

    private void PickUpItem(Item item) {
        _inventory.AddItem(item);
    }
}
