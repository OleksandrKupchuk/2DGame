using UnityEngine;

public class PlayerSword : MonoBehaviour {
    [SerializeField]
    private Player _player;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.TryGetComponent(out Enemy enemy)) {
            enemy.TakeDamage(_player.Attributes.Damage);
        }
    }
}