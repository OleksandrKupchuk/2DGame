using UnityEngine;
using UnityEngine.UI;

public class AddPlayerHealth : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private float _health;
    [SerializeField]
    private Button _addHealthButton;

    private void Start() {
        _player = FindObjectOfType<Player>();
        _addHealthButton.onClick.AddListener(() => {
            _player.HealthController.AddHealth(_health);
        });
    }
}