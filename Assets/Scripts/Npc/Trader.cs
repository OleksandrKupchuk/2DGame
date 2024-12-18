using UnityEngine;

public class Trader : Npc {
    private Player _player;

    [SerializeField]
    private int _commissionPercent;
    [SerializeField]
    private Market _market;

    private void Awake() {
        _market.Init(_commissionPercent);
        _popup.SetActive(false);
        EventManager.CloseInventory += CloseMarket;
    }

    private void OnDestroy() {
        EventManager.CloseInventory -= CloseMarket;
    }

    private void CloseMarket() {
        _market.Disable();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _player = player;
            _popup.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _popup.SetActive(false);
        }
    }

    public override void Interact() {
        _market.Enable();
        _market.SetPlayer(_player);
        _player.Inventory.Open();
    }
}
