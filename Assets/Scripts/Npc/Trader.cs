using UnityEngine;

public class Trader : Npc {
    private Dialog _dialogTrade;

    private Dialog _dialogStoryAboutWife;

    [SerializeField]
    private int _commissionPercent;
    [SerializeField]
    private Market _market;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private DialogView _dialogController;
    [SerializeField]
    private DialogData _dialogDataTrade;
    [SerializeField]
    private DialogData _dialogDataStoryWife;

    private void Awake() {
        IDialogAction _dialogActionOpenMarket = new DialogActionOpenMarket(_inventory, _market, _dialogController);
        _dialogTrade = new Dialog(_dialogDataTrade, _dialogActionOpenMarket);

        _dialogStoryAboutWife = new Dialog(_dialogDataStoryWife, _dialogController);

        _dialogs.Add(_dialogTrade);
        _dialogs.Add(_dialogStoryAboutWife);

        _interactionIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _interactionIcon.SetActive(true);
            //print("interactive SET = " + gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            _interactionIcon.SetActive(false);
            //print("interactive OUT = " + gameObject.name);
        }
    }

    public override void Interact() {
        _dialogController.OpenDialogs(gameObject.name, _dialogs);
    }
}
