public class OpenMarket : IDialogAction {
    private Player _player;
    private Market _market;
    private DialogView _dialogController;

    public OpenMarket(Market market, DialogView dialogController) {
        _market = market;
        _dialogController = dialogController;

    }

    public void DoAction() {
        _player = ProjectContext.Instance.Player;
        _player.PlayerMovement.EnableInput();
        _dialogController.CloseDialogs();
        _market.Open();
    }
}
