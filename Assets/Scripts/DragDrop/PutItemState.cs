public class PutItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;

        _controller.cell.SetItem(_controller.Cursor.Item);
        _controller.cell.SetAvailableForInteraction(true);
        _controller.cell.SetAndEnableIcon(_controller.Cursor.Item.Icon);

        _controller.Cursor.DisableIcon();
        _controller.Cursor.SetItem(null);
        _controller.ChangeState(_controller.CheckItemState);

        _controller.Cursor.TryGetPlayerSlotComponentAndCallEvent(EventManager.PutOnOrTakenAwakeItemEventHandler);
    }

    public void Exit() {
    }

    public void Update() {
    }
}