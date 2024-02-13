public class PutItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;

        _controller.Cursor.Cell.SetItem(_controller.Cursor.Item);
        _controller.Cursor.Cell.SetAvailableForInteraction(true);
        if (_controller.Cursor.IsPlayerSlot()) {
            EventManager.PutOnItemEventHandler(_controller.Cursor.Cell.Item);
        }


        _controller.Cursor.OnTriggerEnter2D(_controller.Cursor.Cell.BoxCollider2D);
        _controller.Cursor.SetItem(null);
        _controller.ChangeState(_controller.CheckItemState);
    }

    public void Exit() {
    }

    public void Update() {
    }
}
