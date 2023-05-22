public class SwapItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;

        Item _bufferItem = _controller.Cursor.Cell.Item;

        _controller.Cursor.Cell.PutItem(_controller.Cursor.Item);

        _controller.Cursor.OnTriggerEnter2D(_controller.Cursor.Cell.BoxCollider2D);
        _controller.Cursor.SetItem(_bufferItem);

        _controller.ChangeState(_controller.RaisedItemState);

        _controller.Cursor.TryGetPlayerSlotComponentAndCallEvent(EventManager.PutOnOrTakenAwakeItemEventHandler);
    }

    public void Exit() {
    }

    public void Update() {
    }
}