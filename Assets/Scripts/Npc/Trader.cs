using UnityEngine;

public class Trader : Npc {
    private Player _player;

    [SerializeField]
    private int _commissionPercent;
    [SerializeField]
    private Market _market;
    [SerializeField]
    private DialogController _dialogController;
    [SerializeField]
    private Dialogs _dialogs;

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
            _dialogController.Show(_dialogs);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
        }
    }

    public override void Interact() {
        _market.Enable();
        _market.SetPlayer(_player);
        _player.Inventory.Open();
    }
}
