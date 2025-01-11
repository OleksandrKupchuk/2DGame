using UnityEngine;

public class PickUpController : MonoBehaviour {
    [SerializeField]
    private Inventory _inventoryController;

    private void OnCollisionEnter2D(Collision2D collision) {
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
}
