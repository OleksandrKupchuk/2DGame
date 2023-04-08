using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out PlayerBodyPart playerBodyPart)) {
            playerBodyPart.TakeDamage(damage, this);
        }
    }
}
