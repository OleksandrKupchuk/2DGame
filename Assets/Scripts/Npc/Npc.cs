using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Npc : MonoBehaviour {
    private Player _player;

    [SerializeField]
    private Dialog _dialog;
    [SerializeField]
    private Market _market;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _player = player;
            //_dialog.Show();
            _market.Enable();
            _market.Init(_player);
            _player.Inventory.Open();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //if (collision.gameObject.tag.Equals("Player")) {
        //    //_dialog.Hide();
        //    _market.Disable();
        //    _player.Inventory.Open();
        //}

        if (collision.gameObject.TryGetComponent(out Player player)) {
            _market.Disable();
            player.Inventory.Open();
        }
    }
}
