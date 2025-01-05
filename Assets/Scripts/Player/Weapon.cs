using UnityEngine;

public class Weapon : MonoBehaviour {
    [field : SerializeField]
    public BoxCollider2D BoxCollider2D { get; private set; }
    [SerializeField]
    private Player _player;
    [SerializeField]
    private DamageAttribute _damageAttribute;

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.transform.TryGetComponent(out Enemy enemy)) {
            enemy.TakeDamage(_damageAttribute.Damage);
        }
    }
}
