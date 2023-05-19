public class PutItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        _controller = controller;

        _controller.Cell.SetItem(_controller.Cursor.Item);
        _controller.Cell.SetAvailableForInteraction(true);
        _controller.Cell.SetAndEnableIcon(_controller.Cursor.Item.Icon);

        _controller.Cursor.OnTriggerEnter2D(_controller.Cursor.Cell.BoxCollider2D);
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