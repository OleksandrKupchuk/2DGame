using UnityEngine;

public class SwapItemState : IDragDropState {
    private DragDropController _controller;

    public void Enter(DragDropController controller) {
        Debug.Log("swap item enter");
        _controller = controller;

        Cell _cell = _controller.Cursor.GetCell();
        Item _bufferItem = _cell.Item;

        _cell.SetItem(_controller.Cursor.Item);

        if (_cell.Item == _controller.Cursor.Item) {
            _controller.Cursor.SetItem(_bufferItem);
        }
        _controller.ChangeState(_controller.RaisedItemState);
        _controller.Cursor.OnTriggerEnter2D(_cell.BoxCollider2D);
    }

    public void Exit() {
        Debug.Log("swap item exit");
    }

    public void Update() {
    }
}
