using UnityEngine;

public class PlayerBodyPart : MonoBehaviour {
    [SerializeField]
    private PlayerHealthController _playerHealthController;

    public void TakeDamage(float damage, Damage damageObject) {
        _playerHealthController.CheckTakeDamage(damage, damageObject);
    }
}
