using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour {
    private Inventory _inventory;
    private Player _player;
    private List<Item> _items = new List<Item>();

    private void Awake() {
        _inventory = FindObjectOfType<Inventory>();
        DropItemState.DropItem += UnregisterPickUpItem;
    }

    private void Start() {
        _player = FindObjectOfType<Player>();
    }

    private void OnDestroy() {
        DropItemState.DropItem -= UnregisterPickUpItem;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.TryGetComponent(out Item item)) {
            if (_items.Contains(item)) {
                return;
            }

            RegisterPickUpItem(item);
            PickUpItem(item);
        }

        if (collision.gameObject.name.Contains("Quest item king")) {
            print("pickup quest item");
            _player.QuestSystem.AddQuestItem(collision.gameObject);
        }
    }

    private void RegisterPickUpItem(Item item) {
        _items.Add(item);
    }

    private void UnregisterPickUpItem(Item item) {
        _items.Remove(item);
    }

    private void PickUpItem(Item item) {
        _inventory.AddItem(item);
    }
}
