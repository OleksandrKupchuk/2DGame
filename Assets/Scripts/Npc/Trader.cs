using UnityEngine;

public class Trader : Npc {
    private IDialog _dialogTrade;
    private IDialog _dialogStory;

    [SerializeField]
    private int _commissionPercent;
    [SerializeField]
    private Market _market;
    [SerializeField]
    private DialogController _dialogController;
    [SerializeField]
    private DialogData _dialogTradeData;
    [SerializeField]
    private DialogData _dialogStoryData;

    private void Awake() {
        _market.Init(_commissionPercent);
        EventManager.CloseInventory += CloseMarket;

        _dialogTrade = new DialogTrade(_dialogTradeData, _market, _dialogController);
        _dialogStory = new DialogStory(_dialogStoryData, _dialogController);

        _dialogs.Add(_dialogTrade);
        _dialogs.Add(_dialogStory);

        _interactionIcon.SetActive(false);
    }

    private void OnDestroy() {
        EventManager.CloseInventory -= CloseMarket;
    }

    private void CloseMarket() {
        _market.Close();
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
        _dialogController.OpenDialogs(_dialogs);
    }
}
