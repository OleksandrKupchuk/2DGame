public class PutItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;

        PlayerSlot _playerSlot = _controller.Cursor.RaycastHit2D.transform.GetComponent<PlayerSlot>();

        _controller.cell.SetItem(_controller.Cursor.Item);
        _controller.cell.SetAvailableForInteraction(true);
        _controller.cell.SetAndEnableIcon(_controller.Cursor.Item.Icon);

        _controller.Cursor.DisableIcon();
        _controller.Cursor.SetItem(null);
        _controller.ChangeState(_controller.CheckItemState);

        if (_playerSlot != null) {
            EventManager.PutOrTakeAwayItemInPlayerSlot();
        }
    }

    public void Exit() {
    }

    public void Update() {
    }
}