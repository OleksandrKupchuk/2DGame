using UnityEngine;

public class PickUp : MonoBehaviour {
    [SerializeField]
    private Inventory _inventory;

    private void Awake() {
        _inventory = FindObjectOfType<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.TryGetComponent(out Item item)) {
            PickUpItem(item);
        }
    }

    private void PickUpItem(Item item) {
        _inventory.PutItemInEmptyCell(item);
    }
}