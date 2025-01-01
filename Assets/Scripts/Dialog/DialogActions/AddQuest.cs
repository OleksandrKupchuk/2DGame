public class AddQuest : IDialogAction {
    private Player _player;
    private IQuest _quest;

    public AddQuest(IQuest quest) {
        _quest = quest;
    }

    public void DoAction() {
        _player = ProjectContext.Instance.Player;
        _player.QuestSystem.AddQuest(_quest);
    }
}
