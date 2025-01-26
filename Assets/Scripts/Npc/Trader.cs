using UnityEngine;

public class Trader : Npc {
    [SerializeField]
    private DialogController _dialogController;
    [SerializeField]
    private Dialogues _dialogues;
    [SerializeField]
    private int _commissionPercent;
    [SerializeField]
    private Market _market;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]

    private void Awake() {
        //IDialogAction _dialogActionOpenMarket = new DialogActionOpenMarket(_inventory, _market, _dialogController);
        //_dialogTrade = new DialogController(_dialogDataTrade, _dialogActionOpenMarket);

        //_dialogStoryAboutWife = new DialogController(_dialogDataStoryWife, _dialogController);

        //_dialogues.Add(_dialogTrade);
        //_dialogues.Add(_dialogStoryAboutWife);

        //_interactionIcon.SetActive(false);
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
        _dialogController.OpenDialogues(gameObject.name, _dialogues);
    }
}
