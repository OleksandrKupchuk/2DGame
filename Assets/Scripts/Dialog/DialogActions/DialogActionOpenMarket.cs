using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogAction", menuName = "DialogActions/OpenMarket")]
public class DialogActionOpenMarket : DialogAction {
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    private Market _market;
    [SerializeField]
    private DialogController _dialogController;

    public override void Execute() {
        _inventory.Open();
        _dialogController.CloseDialogues();
        _market.Open();
    }
}
