public class OpenMarket : IDialogAction {
    private Player _player;
    private Market _market;
    private DialogController _dialogController;

    public OpenMarket(Market market, DialogController dialogController) {
        _market = market;
        _dialogController = dialogController;

    }

    public void DoAction() {
        _player = ProjectContext.Instance.Player;
        _player.PlayerMovement.EnableInput();
        _dialogController.CloseDialogs();
        _market.Open(_player);
    }
}
