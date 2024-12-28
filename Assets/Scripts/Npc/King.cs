using UnityEngine;

public class King : Npc, IInteracvite {
    private IDialog _dialogQuest;
    private IDialog _dialogStory;
    private IDialog _dialogInterestStory;
    private IQuest _questBringItem;

    [SerializeField]
    private DialogController _dialogController;
    [SerializeField]
    private DialogData _dialogQuestData;
    [SerializeField]
    private DialogData _dialogStoryData;
    [SerializeField]
    private DialogData _dialogInterestStoryData;

    private void Awake() {
        _questBringItem = new QuestBringItem();
        _dialogQuest = new DialogStory(_dialogQuestData, _dialogController, _questBringItem);
        _dialogStory = new DialogStory(_dialogStoryData, _dialogController);
        _dialogInterestStory = new DialogStory(_dialogInterestStoryData, _dialogController);
        _dialogs.Add(_dialogQuest);
        _dialogs.Add(_dialogStory);
        _dialogs.Add(_dialogInterestStory);
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
        _dialogController.OpenDialogs(_dialogs);
    }
}
