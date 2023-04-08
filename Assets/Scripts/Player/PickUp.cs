using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    [SerializeField]
    private Inventory _inventory;
    private List<Item> _items = new List<Item>();

    private void Awake() {
        _inventory = FindObjectOfType<Inventory>();
        CellContent.DropItem += SpawnItem;
    }

    private void OnDestroy() {
        CellContent.DropItem -= SpawnItem;
    }

    //private void OnTriggerEnter2D(Collider2D collision) {
    //    if(collision.TryGetComponent(out Item item)) {
    //        if (IsAlredyPickUpThisItem(item)) {
    //            return;
    //        }

    //        RegisterPickUpItem(item);
    //        PickUpItem(item);
    //    }
    //}

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
                print("item was pick up");
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
        _inventory.PutItemInEmptyCell(item);
    }

    private void SpawnItem(Item registeredItem) {
        foreach (var item in _items) {
            if(registeredItem == item) {
                UnregisterPickUpItem(item);
                item.transform.position = new Vector3(transform.position.x - (transform.localScale.x * 5f), transform.position.y + 4, item.transform.position.z);
                item.gameObject.SetActive(true);
                return;
            }
        }
    }
}