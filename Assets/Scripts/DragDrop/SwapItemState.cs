public class SwapItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;

        Item _bufferItem = _controller.cell.Item;

        _controller.cell.SetAndEnableIcon(_controller.Cursor.Item.Icon);
        _controller.cell.SetItem(_controller.Cursor.Item);

        _controller.Cursor.SetItem(_bufferItem);
        _controller.Cursor.SetAndEnableIcon(_bufferItem.Icon);

        _controller.ChangeState(_controller.RaisedItemState);

        _controller.Cursor.TryGetPlayerSlotComponentAndCallEvent(EventManager.PutOnOrTakenAwakeItemEventHandler);
    }

    public void Exit() {
    }

    public void Update() {
    }
}