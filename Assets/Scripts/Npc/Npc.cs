using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Npc : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private int _commission;
    [SerializeField]
    private Dialog _dialog;
    [SerializeField]
    private Market _market;

    private void Awake() {
        _market.Init(_commission);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _player = player;
            _market.Enable();
            _market.SetPlayer(_player);
            _player.Inventory.Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _market.Disable();
            player.Inventory.Close();
        }
    }
}
