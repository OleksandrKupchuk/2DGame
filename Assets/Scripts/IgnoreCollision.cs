using UnityEngine;

public class IgnoreCollision : MonoBehaviour {
    private Player _player;

    public void IgnorePlayerColliders(Collider2D collider1) {
        _player = FindObjectOfType<Player>();

        foreach (Collider2D playerCollider in _player.CollidesForIgnored) {
            Physics2D.IgnoreCollision(collider1, playerCollider, true);
        }
    }
}
