using UnityEngine;

public class King : Npc, IInteracvite {
    [SerializeField]
    private PlayerConfig _playerConfig;
    [SerializeField]
    private DialogView _dialogController;
    [SerializeField]
    private DialogData _dialogDataBringCrown;
    [SerializeField]
    private DialogData _dialogDataKingdom;
    [SerializeField]
    private DialogData _dialogDataIncome;

    private void Awake() {
        IQuest _questBringItem = new QuestBringItem(_playerConfig);
        IDialogAction _actionAddQuestBringCrown = new DialogActionAddQuest(_questBringItem);
        Dialog _dialogBringCrown = new Dialog(_dialogDataBringCrown, _dialogController, _actionAddQuestBringCrown);

        Dialog _dialogKingdom = new Dialog(_dialogDataKingdom, _dialogController);

        Dialog _dialogIncome = new Dialog(_dialogDataIncome, _dialogController);

        _dialogs.Add(_dialogBringCrown);
        _dialogs.Add(_dialogKingdom);
        _dialogs.Add(_dialogIncome);

        _interactionIcon.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            print("interactive SET = " + gameObject.name);
            _interactionIcon.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            print("interactive OUT = " + gameObject.name);
            _interactionIcon.SetActive(false);
        }
    }

    public override void Interact() {
        _dialogController.OpenDialogs(gameObject.name, _dialogs);
    }
}
