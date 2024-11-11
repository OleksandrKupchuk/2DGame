using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    private Inventory _inventory;
    private List<Item> _items = new List<Item>();

    private void Awake() {
        _inventory = FindObjectOfType<Inventory>();
        DropItemState.DropItem += UnregisterPickUpItem;
    }

    private void OnDestroy() {
        DropItemState.DropItem -= UnregisterPickUpItem;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.transform.TryGetComponent(out Item item)) {
            if (IsAlredyPickUpThisItem(item)) {
                return;
            }

            RegisterPickUpItem(item);
            PickUpItem(item);
        }
    }

    private bool IsAlredyPickUpThisItem(Item item) {
        if (_items.Count == 0) {
            return false;
        }

        foreach (var _item in _items) {
            if (_item == item) {
                //print("item was pick up");
                return true;
            }
        }

        return false;
    }

    private void RegisterPickUpItem(Item item) {
        _items.Add(item);
    }

    private void UnregisterPickUpItem(Item item) {
        _items.Remove(item);
    }

    private void PickUpItem(Item item) {
        _inventory.PutItem(item);
    }
}
