public class DialogTrade : IDialog {
    private Market _market;
    private DialogController _dialogController;
    private DialogData _dialogData;

    public DialogData DialogData => _dialogData;

    public DialogTrade(DialogData dialogData, Market market, DialogController dialogController) {
        _dialogData = dialogData;
        _market = market;
        _dialogController = dialogController;
    }

    public void Start() {
        Player _player = ProjectContext.Instance.Player;
        _player.PlayerMovement.EnableInput();
        _dialogController.CloseDialogs();
        _market.Open(_player);
        _player.Inventory.Open();
    }
}
