public class DialogActionAddQuest : IDialogAction {
    private IQuest _quest;

    public DialogActionAddQuest(IQuest quest) {
        _quest = quest;
    }

    public void DoAction() {
        QuestSystem.Instance.AddQuest(_quest);
    }
}
