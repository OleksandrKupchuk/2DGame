public class AddQuest : IDialogAction {
    private IQuest _quest;

    public AddQuest(IQuest quest) {
        _quest = quest;
    }

    public void DoAction() {
        QuestSystem.Instance.AddQuest(_quest);
    }
}
