using UnityEngine;

public class PlayerSword : MonoBehaviour {
    [field : SerializeField]
    public BoxCollider2D BoxCollider2D { get; private set; }
    [SerializeField]
    private Player _player;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.TryGetComponent(out Enemy enemy)) {
            enemy.TakeDamage(_player.Inventory.PlayerAttributes.Damage);
        }
    }
}
