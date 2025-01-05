using UnityEngine;

public class Damage : MonoBehaviour {
    private float _damage;
    public float minDamage;
    public float maxDamage;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out PlayerBodyPart playerBodyPart)) {
            _damage = Random.Range(minDamage, maxDamage);
            playerBodyPart.TakeDamage(_damage, this);
        }
    }
}
