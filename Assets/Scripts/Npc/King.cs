using UnityEngine;

public class King : Npc, IInteracvite {
    private Player _player;
    private IQuestTask _questBringItem;

    [SerializeField]
    private DialogController _dialogController;
    [SerializeField]
    private Dialogs _dialogs;

    private void Awake() {
        _popup.SetActive(false);
        _questBringItem = new QuestBringItem();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
            print("king touch player");
            _player = player;
            _dialogController.Show(_dialogs);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out Player player)) {
        }
    }

    public override void Interact() {
        _player.QuestSystem.AddQuest(_questBringItem);
    }
}
