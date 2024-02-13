public class SwapItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;

        Item _bufferItem = _controller.Cursor.Cell.Item;

        //if (_controller.Cursor.IsPlayerSlot()) {
        //    EventManager.TakeAwayItemEventHandler(_controller.Cursor.Cell.Item);
        //}

        _controller.Cursor.Cell.SetItem(_controller.Cursor.Item);
        if (_controller.Cursor.IsPlayerSlot()) {
            EventManager.PutOnItemEventHandler(_controller.Cursor.Item);
        }

        _controller.Cursor.OnTriggerEnter2D(_controller.Cursor.Cell.BoxCollider2D);
        _controller.Cursor.SetItem(_bufferItem);

        _controller.ChangeState(_controller.RaisedItemState);

    }

    public void Exit() {
    }

    public void Update() {
    }
}
