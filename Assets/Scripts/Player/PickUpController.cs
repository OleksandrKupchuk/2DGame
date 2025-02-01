using UnityEngine;

public class PickUpController : MonoBehaviour {
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private QuestSystem _questSystem;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name.Contains("DialogAction item king")) {
            print("pickup quest item");
            _questSystem.AddQuestItem(collision.gameObject);
        }

        if (collision.transform.TryGetComponent(out ItemView itemView)) {
            if (_inventory.TryAddItem(itemView.ItemData)) {
                Destroy(itemView.gameObject);
            }
        }
    }
}
