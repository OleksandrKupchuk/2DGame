public class DialogActionOpenMarket : IDialogAction {
    private Inventory _inventory;
    private Market _market;
    private DialogView _dialogController;

    public DialogActionOpenMarket(Inventory inventory, Market market, DialogView dialogController) {
        _inventory = inventory;
        _market = market;
        _dialogController = dialogController;

    }

    public void DoAction() {
        _inventory.Open();
        _dialogController.CloseDialogs();
        _market.Open();
    }
}
